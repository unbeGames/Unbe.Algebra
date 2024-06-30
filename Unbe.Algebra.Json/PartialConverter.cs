using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Unbe.Algebra.Json {
	public abstract class PartialConverter<T> : JsonConverter {
    /// <summary>
    /// The stored property names with the member.
    /// </summary>
    private static Dictionary<string, MemberInfo> properties;

    /// <summary>
    /// Get the field or property of the specified <c>name</c>.
    /// </summary>
    /// <returns>The member.</returns>
    /// <param name="name">Name.</param>
    private static MemberInfo GetMember(string name) {
			var flag = BindingFlags.Instance | BindingFlags.Public;

			var field = typeof(T).GetField(name, flag);
			if (field != null) 
				return field;

			var property = typeof(T).GetProperty(name, flag);
			if (property == null) Throw(name, "Public instance field or property {0} is not found.");

			if (property.GetGetMethod() == null) Throw(name, "Property {0} is not readable.");
			if (property.GetSetMethod() == null) Throw(name, "Property {0} is not writable.");

			if (property.GetIndexParameters().Any()) Throw(name, "Not support property {0} with indexes.");
			return property;
		}

		/// <summary>
		/// Throw an exception of the specified message formatted with the member name.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="format">Format.</param>
		private static void Throw(string name, string format) {
			throw new ArgumentException(string.Format(format, $"{typeof(T).Name}.{name}"), nameof(name));
		}

		/// <summary>
		/// Get the value from the member.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="member">Member.</param>
		/// <param name="target">Target.</param>
		private static object GetValue(MemberInfo member, object target) {
			if (member is FieldInfo) {
				return (member as FieldInfo).GetValue(target);
			} else { 
				return (member as PropertyInfo).GetValue(target, null);
			} 
		}

		/// <summary>
		/// Set the value to the member.
		/// </summary>
		/// <param name="member">Member.</param>
		/// <param name="target">Target.</param>
		/// <param name="value">Value.</param>
		private static void SetValue(MemberInfo member, object target, object value) {
			if (member is FieldInfo) {
				(member as FieldInfo).SetValue(target, value);
			} else { 
				(member as PropertyInfo).SetValue(target, value, null);
			} 
		}

		/// <summary>
		/// Get the value type of the member.
		/// </summary>
		/// <returns>The value type.</returns>
		/// <param name="member">Member.</param>
		private static Type GetValueType(MemberInfo member) {
			if (member is FieldInfo) {
				return (member as FieldInfo).FieldType;
			} else { 
				return (member as PropertyInfo).PropertyType;
			}
		}

		/// <summary>
		/// Gets the property names paired with the accessing member.
		/// </summary>
		/// <returns>The properties.</returns>
		private Dictionary<string, MemberInfo> GetProperties() {
			if (properties != null) 
				return properties;

			var names = GetPropertyNames();

			if (names == null || names.Length == 0)
				throw new InvalidProgramException("GetPropertyNames() cannot return empty.");

			if (names.Any(string.IsNullOrEmpty))
				throw new InvalidProgramException("GetPropertyNames() cannot contain empty value.");

			properties = names.Distinct().ToDictionary((name) => name, GetMember);
			return properties;
		}

		/// <summary>
		/// Get the property names to serialize, only used once when initializing.
		/// </summary>
		/// <returns>The property names.</returns>
		protected abstract string[] GetPropertyNames();
		

		/// <summary>
		/// Determine if the object type is <c>T</c>.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns><c>true</c> if this can convert the specified type; otherwise, <c>false</c>.</returns>
		public override bool CanConvert(Type objectType) {
			return typeof(T) == objectType;
		}

		/// <summary>
		/// Read the specified properties to the object.
		/// </summary>
		/// <returns>The object value.</returns>
		/// <param name="reader">The <c>Newtonsoft.Json.JsonReader</c> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/*
		 * Force the instance as an object reference, otherwise this may reflect to a wrong copy if the T is struct.
		 * But keep the CreateInstance() to return T for safer overriding.
		 */
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			if (reader.TokenType == JsonToken.Null) 
				return null;

			var obj = JObject.Load(reader);
			var result = Activator.CreateInstance<T>() as object;

			foreach (var pair in GetProperties()) {
				if (obj[pair.Key] != null) {
					var valueType = GetValueType(pair.Value);
          var value = obj[pair.Key].ToObject(valueType, serializer);
					SetValue(pair.Value, result, value);
				}
			}

			return result;
		}

		/// <summary>
		/// Write the specified properties of the object.
		/// </summary>
		/// <param name="writer">The <c>Newtonsoft.Json.JsonWriter</c> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			var obj = new JObject();

			foreach (var pair in GetProperties()) {
				var val = GetValue(pair.Value, value);
				obj[pair.Key] = JToken.FromObject(val, serializer);
			}

			obj.WriteTo(writer);
		}
	}
}
