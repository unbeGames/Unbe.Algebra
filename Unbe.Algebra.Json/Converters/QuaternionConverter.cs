namespace Unbe.Algebra.Json {	
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>Quaternion</c>.
	/// </summary>
	public class QuaternionConverter : PartialConverter<Quaternion>{ 

		/// <summary>
		/// Get the property name <c>value</c>.
		/// </summary>
		/// <returns>The property names.</returns>
		protected override string[] GetPropertyNames(){
			return new []{ "x", "y", "z", "w" };
		}
	}
}
