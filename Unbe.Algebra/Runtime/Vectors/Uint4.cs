#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensions: 4)] 
  public partial struct Uint4 : IEquatable<Uint4>, IFormattable {
    public Vector128<uint> value;
  }
}
