#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Math {
  [MathType(type: typeof(uint), dimensions: 4)] 
  public partial struct uint4 : IEquatable<uint4>, IFormattable {
    public Vector128<uint> value;

    /// <summary>Returns the result of a componentwise left shift operation on a uint4 vector by a number of bits specified by a single int.</summary>
    /// <param name="x">The vector to left shift.</param>
    /// <param name="n">The number of bits to left shift.</param>
    /// <returns>The result of the componentwise left shift.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint4 operator <<(uint4 x, int n) { return new uint4(Vector128.ShiftLeft(x.value, n)); }

    /// <summary>Returns the result of a componentwise right shift operation on a uint4 vector by a number of bits specified by a single int.</summary>
    /// <param name="x">The vector to right shift.</param>
    /// <param name="n">The number of bits to right shift.</param>
    /// <returns>The result of the componentwise right shift.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint4 operator >>(uint4 x, int n) { return new uint4(Vector128.ShiftRightLogical(x.value, n)); }

  }
}
