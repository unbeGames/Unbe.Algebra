#pragma warning disable IDE1006

using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 4)] 
  public partial struct UInt4 : IEquatable<UInt4>, IFormattable, IMinMaxValue<UInt4> {
    public Vector128<uint> value;
  }
}
