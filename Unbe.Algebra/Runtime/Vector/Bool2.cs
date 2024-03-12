using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 2)]
  public partial struct Bool2 : IEquatable<Bool2> {
    public Vector64<int> value;

    /// <summary>Constructs a Bool3 vector from a UInt3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool2(UInt2 v) {
      value = v.value.AsInt32();
    }

    /// <summary>Constructs a Bool3 vector from a Int3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool2(Int2 v) {
      value = v.value;
    }

    /// <summary>Constructs a Bool3 vector from a Float3 vector.</summary>
    /// <param name="v">The constructed vector's components will be set to this value.</param>
    public Bool2(Float2 v) {
      value = Vector64.Create((int)v[0], (int)v[1]);
    }
  }
}
