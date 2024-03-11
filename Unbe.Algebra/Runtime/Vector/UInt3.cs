using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 3)] 
  public partial struct UInt3 : IEquatable<UInt3>, IFormattable, IMinMaxValue<UInt3> {
    public Vector128<uint> value;

    /// <summary>Constructs a UInt3 vector from a Int3 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    public UInt3(Int3 v) {
      value = v.value.AsUInt32();
    }

    /// <summary>Constructs a UInt3 vector from a Float4 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    public UInt3(Float3 v) {
      value = Vector128Ext.ConvertToInt32(v.value).AsUInt32();
    }
  }
}
