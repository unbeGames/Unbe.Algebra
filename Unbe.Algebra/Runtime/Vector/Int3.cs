using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 3)]
  public partial struct Int3 : IEquatable<Int3>, IFormattable {
    public Vector128<int> value;
  }
}
