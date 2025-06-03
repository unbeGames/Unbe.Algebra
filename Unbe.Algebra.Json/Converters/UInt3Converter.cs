namespace Unbe.Algebra.Json {
  /// <summary>
  /// Custom <c>Newtonsoft.Json.JsonConverter</c> for <c>UInt3</c>.
  /// </summary>
  public class UInt3Converter : PartialConverter<UInt3> {
    /// <summary>
    /// Get the property names include <c>x</c>, <c>y</c>, <c>z</c>
    /// </summary>
    /// <returns>The property names.</returns>
    protected override string[] GetPropertyNames() {
      return ["x", "y", "z"];
    }
  }
}
