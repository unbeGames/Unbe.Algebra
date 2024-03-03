#pragma warning disable IDE1006

using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 4)]
  public partial struct Int4 : IEquatable<Int4>, IFormattable, IMinMaxValue<Int4> {
    public Vector128<int> value;
  }
}
