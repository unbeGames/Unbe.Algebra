using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 3)]
  public partial struct Int3 : IEquatable<Int3>, IFormattable, IMinMaxValue<Int3> {
    public Vector128<int> value;
  }
}
