#pragma warning disable IDE1006

using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 4)] 
  public partial struct UInt4 : IEquatable<UInt4>, IFormattable, IMinMaxValue<UInt4> {
    public Vector128<uint> value;

    /// <summary>Constructs a UInt4 vector from a Int4 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    public UInt4(Int4 v) {
      value = v.value.AsUInt32();
    }

    /// <summary>Constructs a UInt4 vector from a Float4 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    public UInt4(Float4 v) {
      value = Vector128Ext.ConvertToInt32(v.value).AsUInt32();
    }
  }
}
