#pragma warning disable 8981

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using half = System.Half;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 2)]
  public partial struct Float2 : IEquatable<Float2>, IFormattable { 
    public Vector64<float> value;

    /// <summary>Constructs a Float2 vector from a single half value by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float2</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float2(half v) {
      value = Vector64.Create((float)v);
    }

    /// <summary>Constructs a Float2 vector from a Vector4.</summary>
    /// <param name="v">Vector2 to convert to Float2</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float2(Vector2 vector) {
      value = Vector64.Create(vector.X, vector.Y);
    }

    /// <summary>Implicitly converts a single half value to a Float2 vector by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float2</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float2(half v) { return new Float2(v); }

    /// <summary>Constructs a Float2 vector from a Vector2.</summary>
    /// <param name="v">Vector2 to convert to Float2</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float2(Vector2 v) { return new Float2(v); }

    /// <summary>Constructs a Float2 vector from a Vector2.</summary>
    /// <param name="v">Vector2 to convert to Float2</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector2(Float2 v) { return Unsafe.As<Vector64<float>, Vector2>(ref v.value); }
  }
}
