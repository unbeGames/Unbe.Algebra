using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 3)]
  public partial struct Bool3 : IEquatable<Bool3> {
    public Vector128<int> value;

    /// <summary>Constructs a Bool3 vector from a UInt3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool3(UInt3 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Bool3 vector from a Int3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool3(Int3 v) {
      value = v.value;
    }

    /// <summary>Constructs a Bool3 vector from a Float3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool3(Float3 v) {
      value = Vector128Ext.ConvertToInt32(v.value);
    }
  }
}
