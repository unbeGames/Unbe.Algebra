namespace Unbe.Algebra.Json {
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>Int3</c>.
	/// </summary>
	public class Int3Converter : PartialConverter<Int3> {
		/// <summary>
		/// Get the property names include <c>x</c>, <c>y</c>, <c>z</c>.
		/// </summary>
		/// <returns>The property names.</returns>
		protected override string[] GetPropertyNames(){
			return new []{"x", "y", "z"};
		}
	}	
}
