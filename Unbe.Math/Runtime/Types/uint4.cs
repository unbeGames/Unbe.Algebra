#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Math {
  [MathType(type: typeof(uint), dimensions: 4)] 
  public partial struct uint4 : IEquatable<uint4>, IFormattable {
    public Vector128<uint> value;
  }
}
