#pragma warning disable IDE1006

using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 4)] 
  public partial struct UInt4 : IEquatable<UInt4>, IFormattable {
    public Vector128<uint> value;
  }
}
