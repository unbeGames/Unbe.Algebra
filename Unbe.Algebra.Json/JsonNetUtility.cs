using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unbe.Algebra.Json {
	
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> <a href="http://www.newtonsoft.com/json" target="_blank">Json.NET</a>.
	/// </summary>
	/// 
	/// 
	/// <example>
	/// Now we can use Json.NET just like before:
	/// </example>
	/// 
	/// <code>
	/// Log.Info(JsonConvert.SerializeObject(Vector3.up));
	/// var vec = JsonConvert.DeserializeObject<Vector2>("{'x':1.0,'y':0.0}");
	/// </code>
	/// 
	/// <example>
	/// User can directly modify <c>defaultSettings</c> for customization, and override it:
	/// </example>
	/// 
	/// <code>
	/// JsonConvert.DefaultSettings = () => new JsonSerializerSettings(){
	/// 	Converters = JsonNetUtility.defaultSettings.Converters,
	/// 	DefaultValueHandling = DefaultValueHandling.Populate
	/// };
	/// </code>
	/// 
	public static class JsonNetUtility{

		#region Fields

		/// <summary>
		/// The default <c>Newtonsoft.Json.JsonSerializerSettings</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// All its properties stay default, but the <c>Converters</c> includes below:
		/// 	1. Any custom <c>Newtonsoft.Json.JsonConverter</c> has constructor without parameters.
		/// 	2. Any <c>Newtonsoft.Json.JsonConverter</c> from <c>Unbe.Algebra.Json</c>.
		/// 	3. <c>Newtonsoft.Json.Converters.StringEnumConverter</c>.
		/// 	4. <c>Newtonsoft.Json.Converters.VersionConverter</c>.
		/// </remarks>
		/// 
		public static JsonSerializerSettings defaultSettings = new JsonSerializerSettings(){
			Converters = CreateConverters()
		};

		#endregion


		#region Methods

		/// <summary>
		/// Initialize when start up, set <c>Newtonsoft.Json.JsonConvert.DefaultSettings</c> if not yet.
		/// </summary>
		public static void Initialize(){
			if(null == JsonConvert.DefaultSettings) JsonConvert.DefaultSettings = () => defaultSettings;
		}

		/// <summary>
		/// Create the converter instances.
		/// </summary>
		/// <returns>The converters.</returns>
		private static List<JsonConverter> CreateConverters(){

			var _customs = FindConverterTypes().Select((type) => CreateConverter(type));

			var _builtins = new JsonConverter[]{new StringEnumConverter(), new VersionConverter()};

			return _customs.Concat(_builtins).Where((converter) => null != converter).ToList();

		}

		/// <summary>
		/// Try to create the converter of specified type.
		/// </summary>
		/// <returns>The converter.</returns>
		/// <param name="type">Type.</param>
		private static JsonConverter CreateConverter(Type type){
			
			try{ return Activator.CreateInstance(type) as JsonConverter; }

			catch(Exception exception){ Console.WriteLine("Can't create JsonConverter {0}:\n{1}", type, exception); }

			return null;

		}

		/// <summary>
		/// Find all the valid converter types.
		/// </summary>
		/// <returns>The types.</returns>
		private static Type[] FindConverterTypes(){
			
			return AppDomain.CurrentDomain.GetAssemblies(

				).SelectMany((dll) => dll.GetTypes()
				).Where((type) => typeof(JsonConverter).IsAssignableFrom(type)

				).Where((type) => (!type.IsAbstract && !type.IsGenericTypeDefinition)
				).Where((type) => null != type.GetConstructor(new Type[0])

				).Where((type) => !(null != type.Namespace && type.Namespace.StartsWith("Newtonsoft.Json"))
				).OrderBy((type) => null != type.Namespace && type.Namespace.StartsWith("WanzyeeStudio")
				
			).ToArray();

		}

		#endregion
	}
}
