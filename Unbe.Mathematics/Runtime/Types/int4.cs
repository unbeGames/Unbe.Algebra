#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Math {
  [MathType(type: typeof(int), dimensions: 4)]
  public partial struct int4 : IEquatable<int4>, IFormattable {
    public Vector128<int> value;
  }
}
