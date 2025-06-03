namespace Unbe.Algebra.Json {
  /// <summary>
  /// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>UInt2</c>.
  /// </summary>
  public class UInt2Converter : PartialConverter<UInt2> {
    /// <summary>
    /// Get the property names include <c>x</c>, <c>y</c>
    /// </summary>
    /// <returns>The property names.</returns>
    protected override string[] GetPropertyNames() {
      return ["x", "y"];
    }
  }
}
