#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Math {
  [MathType(type: typeof(int), dimensions: 3)]
  public partial struct int3 : IEquatable<int3>, IFormattable {
    public Vector128<int> value;
  }
}
