#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensions: 2)]
  public partial struct int2 : IEquatable<int2>, IFormattable {
    public Vector64<int> value;
  }
}
