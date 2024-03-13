#pragma warning disable IDE1006, CA1822

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static Unbe.Algebra.Math;

namespace Unbe.Algebra {
  [MathType(type: typeof(bool), dimensionX: 2)]
  public partial struct Bool2 : IEquatable<Bool2> {
    public Vector64<int> value;

    /// <summary>x component of the vector.</summary>
    public unsafe bool x { readonly get { return this[(uint)0] == TRUE; } set { this[(uint)0] = TRUE * *(byte*)&value; } }
    /// <summary>y component of the vector.</summary>
    public unsafe bool y { readonly get { return this[(uint)1] == TRUE; } set { this[(uint)1] = TRUE * *(byte*)&value; } }

    /// <summary>Number of elements in the vector.</summary>
    public readonly int count { get { return 2; } }

    public unsafe bool this[int index] {
      readonly get { return this[(uint)index] == TRUE; }
      set { this[(uint)index] = TRUE * *(byte*)&value; }
    }

    /// <summary>Constructs a Bool4 vector from a single bool value.</summary>
    /// <param name="v">bool to convert to Bool4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool2(bool v) {
      value = Vector64.Create(TRUE * *(byte*)&v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Bool2(Vector64<int> v) {
      value = v;
    }

    /// <summary>Constructs a Bool2 vector from two bool values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <returns>Bool2 constructed from arguments.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe Bool2(bool x, bool y) {
      value = Vector64.Create(TRUE) * Vector64.Create(*(byte*)&x, *(byte*)&y);
    }
  }
}
