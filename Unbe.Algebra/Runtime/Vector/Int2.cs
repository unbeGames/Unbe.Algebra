using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 2)]
  public partial struct Int2 : IEquatable<Int2>, IFormattable, IMinMaxValue<Int2> {
    public Vector64<int> value;

    /// <summary>Constructs a Int2 vector from a UInt2 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int2(UInt2 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Int2 vector from a Float2 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int2(Float2 v) {
      value = Vector64.Create((int)v[0], (int)v[1]);
    }
  }
}
