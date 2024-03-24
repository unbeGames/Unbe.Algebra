namespace Unbe.Algebra.Json {
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>Float2</c>.
	/// </summary>
	public class Float2Converter : PartialConverter<Float2> {
		/// <summary>
		/// Get the property names include <c>x</c>, <c>y</c>
		/// </summary>
		/// <returns>The property names.</returns>
		protected override string[] GetPropertyNames(){
			return new []{"x", "y"};
		}
	}	
}
