using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 2)]
  public partial struct Int2 : IEquatable<Int2>, IFormattable {
    public Vector64<int> value;
  }
}
