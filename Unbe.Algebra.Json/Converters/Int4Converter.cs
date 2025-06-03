namespace Unbe.Algebra.Json {
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>Int4</c>.
	/// </summary>
	public class Int4Converter : PartialConverter<Int4> {
    /// <summary>
    /// Get the property names include <c>x</c>, <c>y</c>, <c>z</c>, <c>w</c>.
    /// </summary>
    /// <returns>The property names.</returns>
    protected override string[] GetPropertyNames(){
			return ["x", "y", "z", "w"];
		}
	}	
}
