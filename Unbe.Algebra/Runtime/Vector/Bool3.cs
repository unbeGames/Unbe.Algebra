#pragma warning disable IDE1006, CA1822

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static Unbe.Algebra.Math;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 3)]
  public partial struct Bool3 : IEquatable<Bool3> {
    public Vector128<int> value;

    /// <summary>x component of the vector.</summary>
    public unsafe bool x { readonly get { return this[0] == TRUE; } set { this[0] = TRUE * *(byte*)&value; } }
    /// <summary>y component of the vector.</summary>
    public unsafe bool y { readonly get { return this[1] == TRUE; } set { this[1] = TRUE * *(byte*)&value; } }
    /// <summary>z component of the vector.</summary>
    public unsafe bool z { readonly get { return this[2] == TRUE; } set { this[2] = TRUE * *(byte*)&value; } }

    /// <summary>Number of elements in the vector.</summary>
    public readonly int count { get { return 3; } }

    /// <summary>Constructs a Bool4 vector from a single bool value.</summary>
    /// <param name="v">bool to convert to Bool4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool3(bool v) {
      int conv = TRUE * *(byte*)&v;
      value = Vector128.Create(conv, conv, conv, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Bool3(Vector128<int> v) {
      value = v;
    }

    /// <summary>Constructs a Bool3 vector from three bool values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <returns>Bool3 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool3(bool x, bool y, bool z) {
      value = Vector128.Create(TRUE) * Vector128.Create(*(byte*)&x, *(byte*)&y, *(byte*)&z, 0);
    }

    /// <summary>Returns a Bool3 vector constructed from a Bool2 vector and a bool value.</summary>
    /// <param name="xy">The constructed vector's xy components will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <returns>Bool3 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool3(Bool2 xy, bool z) {
      value = Vector128.Create(xy[0], xy[1], TRUE * *(byte*)&z, 0);
    }

    /// <summary>Returns a Bool3 vector constructed from bool value and a Bool2 vector.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="yz">The constructed vector's yz components will be set to this value.</param>
    /// <returns>Bool3 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool3(bool x, Bool2 yz) {
      value = Vector128.Create(TRUE * *(byte*)&x, yz[0], yz[1], 0);
    }
  }
}
