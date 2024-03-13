using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 3)]
  public partial struct Int3 : IEquatable<Int3>, IFormattable, IMinMaxValue<Int3> {
    public Vector128<int> value;

    /// <summary>Constructs a Int3 vector from a UInt3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int3(UInt3 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Int3 vector from a Float3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int3(Float3 v) {
      value = Vector128Ext.ConvertToInt32(v.value);
    }

    /// <summary>Constructs a Int3 vector from a Bool3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Int3(Bool3 v) {
      value = Vector128.Abs(v.value);
    }
  }
}
