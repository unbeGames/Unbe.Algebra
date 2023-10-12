#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 2)] 
  public partial struct Uint2 : IEquatable<Uint2>, IFormattable {
    public Vector64<uint> value;
  }
}
