namespace Unbe.Algebra.Json {
  /// <summary>
  /// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>UInt4</c>.
  /// </summary>
  public class UInt4Converter : PartialConverter<UInt4> {
    /// <summary>
    /// Get the property names include <c>x</c>, <c>y</c>, <c>z</c>, <c>w</c>.
    /// </summary>
    /// <returns>The property names.</returns>
    protected override string[] GetPropertyNames() {
      return ["x", "y", "z", "w"];
    }
  }
}
