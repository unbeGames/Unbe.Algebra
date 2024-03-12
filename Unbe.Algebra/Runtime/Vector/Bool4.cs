using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Numerics;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 4)]
  public partial struct Bool4 : IEquatable<Bool4> {
    public Vector128<int> value;

    /// <summary>Constructs a Int4 vector from a UInt4 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool4(UInt4 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Int4 vector from a UInt4 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool4(Int4 v) {
      value = v.value;
    }

    /// <summary>Constructs a Int4 vector from a Float4 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool4(Float4 v) {
      value = Vector128Ext.ConvertToInt32(v.value);
    }
  }
}
