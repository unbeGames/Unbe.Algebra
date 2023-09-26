#pragma warning disable 0660, 0661, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Mathematics2 {
  [MathType(type: typeof(uint), dimensions: 4)] 
  public partial struct uint4 {
    public Vector128<uint> value;

    /// <summary>x component of the vector.</summary>
    public uint x { readonly get { return value.GetElement(0); } set { this.value = this.value.WithElement(0, value); } }
    /// <summary>y component of the vector.</summary>
    public uint y { readonly get { return value.GetElement(1); } set { this.value = this.value.WithElement(1, value); } }
    /// <summary>z component of the vector.</summary>
    public uint z { readonly get { return value.GetElement(2); } set { this.value = this.value.WithElement(2, value); } }
    /// <summary>w component of the vector.</summary>
    public uint w { readonly get { return value.GetElement(3); } set { this.value = this.value.WithElement(3, value); } }

    /// <summary>Constructs a uint4 vector from four uint values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint4(uint x, uint y, uint z, uint w) {
      value = Vector128.Create(x, y, z, w);
    }

    /// <summary>Constructs a uint4 vector from a single uint value by assigning it to every component.</summary>
    /// <param name="v">uint to convert to uint4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint4(uint v) {
      value = Vector128.Create(v);
    }

    /// <summary>Constructs a uint4 vector from a Vector128<uint>.</summary>
    /// <param name="v">Vector128<uint> to convert to uint4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint4(Vector128<uint> v) {
      value = v;
    }

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

    /// <summary>Returns the result of a componentwise bitwise or operation on a uint value and a uint4 vector.</summary>
    /// <param name="lhs">Left hand side uint to use to compute componentwise bitwise or.</param>
    /// <param name="rhs">Right hand side uint4 to use to compute componentwise bitwise or.</param>
    /// <returns>uint4 result of the componentwise bitwise or.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint4 operator |(uint lhs, uint4 rhs) { return new uint4(Vector128.BitwiseOr(Vector128.Create(lhs), rhs.value)); }

  }
}
