using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 4)]
  public partial struct Int4 : IEquatable<Int4>, IFormattable, IMinMaxValue<Int4> {
    public Vector128<int> value;

    /// <summary>Constructs a Int4 vector from a UInt4 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int4(UInt4 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Int4 vector from a Float4 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int4(Float4 v) {
      value = Vector128Ext.ConvertToInt32(v.value);
    }
  }
}
