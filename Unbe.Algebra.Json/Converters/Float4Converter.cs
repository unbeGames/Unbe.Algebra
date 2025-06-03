namespace Unbe.Algebra.Json {
	/// <summary>
	/// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>Float4</c>.
	/// </summary>
	public class Float4Converter : PartialConverter<Float4> {
    /// <summary>
    /// Get the property names include <c>x</c>, <c>y</c>, <c>z</c>, <c>w</c>.
    /// </summary>
    /// <returns>The property names.</returns>
    protected override string[] GetPropertyNames(){
			return ["x", "y", "z", "w"];
		}
	}	
}
