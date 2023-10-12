#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 3)] 
  public partial struct Uint3 : IEquatable<Uint3>, IFormattable {
    public Vector128<uint> value;
  }
}
