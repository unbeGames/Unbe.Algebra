#pragma warning disable IDE1006, CA1822

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static Unbe.Algebra.Math;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 4)]
  public partial struct Bool4 : IEquatable<Bool4> {
    public Vector128<int> value;

    /// <summary>x component of the vector.</summary>
    public unsafe bool x { readonly get { return this[0] == TRUE; } set { this[0] = TRUE * *(byte*)&value; } }
    /// <summary>y component of the vector.</summary>
    public unsafe bool y { readonly get { return this[1] == TRUE; } set { this[1] = TRUE * *(byte*)&value; } }
    /// <summary>z component of the vector.</summary>
    public unsafe bool z { readonly get { return this[2] == TRUE; } set { this[2] = TRUE * *(byte*)&value; } }
    /// <summary>w component of the vector.</summary>
    public unsafe bool w { readonly get { return this[3] == TRUE; } set { this[3] = TRUE * *(byte*)&value; } }


    /// <summary>Number of elements in the vector.</summary>
    public readonly int count { get { return 4; } }

    /// <summary>Constructs a Bool4 vector from a single bool value.</summary>
    /// <param name="v">bool to convert to Bool4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(bool v){
      value = Vector128.Create(TRUE * *(byte*)&v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Bool4(Vector128<int> v) {
      value = v;
    }

    /// <summary>Constructs a Bool4 vector from four bool values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(bool x, bool y, bool z, bool w) {
      value = Vector128.Create(TRUE) * Vector128.Create(*(byte*)&x, *(byte*)&y, *(byte*)&z, *(byte*)&w);
    }

    /// <summaryConstructs a Bool4 vector from a Bool3 vector and a bool value.</summary>
    /// <param name="xyz">The constructed vector's xyz components will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(Bool3 xyz, bool w) {
      value = Vector128.Create(xyz[0], xyz[1], xyz[2], TRUE * *(byte*)&w);
    }

    /// <summaryConstructs a Bool4 vector from a bool value and Bool3 vector.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="yzw">The constructed vector's yzw components will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(bool x, Bool3 yzw) {
      value = Vector128.Create(TRUE * *(byte*)&x, yzw[0], yzw[1], yzw[2]);
    }

    /// <summary>Returns a Bool4 vector constructed from two Bool2 vectors.</summary>
    /// <param name="xy">The constructed vector's xy components will be set to this value.</param>
    /// <param name="zw">The constructed vector's zw components will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Bool4(Bool2 xy, Bool2 zw) {
      value = Vector128.Create(xy[0], xy[1], zw[0], zw[1]);
    }

    /// <summary>Returns a Bool4 vector constructed from a Bool2 vector and two bool values.</summary>
    /// <param name="xy">The constructed vector's xy components will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(Bool2 xy, bool z, bool w) {
      value = Vector128.Create(xy[0], xy[1], TRUE * *(byte*)&z, TRUE * *(byte*)&w);
    }

    /// <summary>Returns a Bool4 vector constructed from a Bool2 vector and two bool values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="yz">The constructed vector's yz components will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(bool x, Bool2 yz, bool w) {
      value = Vector128.Create(TRUE * *(byte*)&x, yz[0], yz[1], TRUE * *(byte*)&w);
    }

    /// <summary>Returns a Bool4 vector constructed from a Bool2 vector and two bool values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <param name="zw">The constructed vector's zw components will be set to this value.</param>
    /// <returns>Bool4 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool4(bool x, bool y, Bool2 zw) {
      value = Vector128.Create(TRUE * *(byte*)&x, TRUE * *(byte*)&y, zw[0], zw[1]);
    }    
  }
}
