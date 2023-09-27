#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Math {
  [MathType(type: typeof(uint), dimensions: 2)] 
  public partial struct uint2 : IEquatable<uint2>, IFormattable {
    public Vector64<uint> value;
  }
}
