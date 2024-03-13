#pragma warning disable 8981

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using half = System.Half;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 4)]
  public partial struct Float4 : IEquatable<Float4>, IFormattable, IMinMaxValue<Float4> { 
    public Vector128<float> value;

    /// <summary>Constructs a Float4 vector from a single half value by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float4</param>
    /// <returns>Constructed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float4(half v) {
      value = Vector128.Create((float)v);
    }

    /// <summary>Constructs a Float4 vector from a Vector4.</summary>
    /// <param name="v">Vector4 to convert to Float4</param>
    /// <returns>Constructed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float4(Vector4 vector) {
      value = Vector128.Create(vector.X, vector.Y, vector.Z, vector.W);
    }

    /// <summary>Constructs a Float4 vector from a Int4 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Float4(Int4 v) {
      value = Vector128Ext.ConvertToSingle(v.value);
    }

    /// <summary>Constructs a Float4 vector from a UInt4 vector.</summary>
    /// <param name="vector">The constructed vector's components will be set to this value.</param>
    /// <returns>Constructed value.</returns>
    public Float4(UInt4 v) {
      value = Vector128Ext.ConvertToSingle(v.value.AsInt32());
    }

    /// <summary>Implicitly converts a single half value to a Float4 vector by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float4</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float4(half v) { return new Float4(v); }

    /// <summary>Constructs a Float4 vector from a Vector4.</summary>
    /// <param name="v">Vector4 to convert to Float4</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float4(Vector4 v) { return new Float4(v); }

    /// <summary>Constructs a Float4 vector from a Vector4.</summary>
    /// <param name="v">Vector4 to convert to Float4</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector4(Float4 v) { return v.value.AsVector4(); }
  }
}
