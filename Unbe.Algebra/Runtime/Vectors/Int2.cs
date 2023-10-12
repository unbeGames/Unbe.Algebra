#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensions: 2)]
  public partial struct Int2 : IEquatable<Int2>, IFormattable {
    public Vector64<int> value;
  }
}
