using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 2)] 
  public partial struct UInt2 : IEquatable<UInt2>, IFormattable, IMinMaxValue<UInt2> {
    public Vector64<uint> value;

    /// <summary>Constructs a UInt2 vector from a Int2 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public UInt2(Int2 v) {
      value = v.value.AsUInt32();
    }

    /// <summary>Constructs a UInt2 vector from a Float2 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public UInt2(Float2 v) {
      value = Vector64.Create((uint)v[0], (uint)v[1]);
    }

    /// <summary>Constructs a UInt2 vector from a Bool2 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public UInt2(Bool2 v) {
      value = Vector64.Abs(v.value).AsUInt32();
    }
  }
}
