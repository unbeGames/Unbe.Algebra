#pragma warning disable 0660, 0661, 8981, IDE1006

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using half = System.Half;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 3)]
  public partial struct Float3 : IEquatable<Float3>, IFormattable { 
    public Vector128<float> value;

    /// <summary>Constructs a Float3 vector from a single half value by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float3</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float3(half v) {
      value = Vector128.Create((float)v);
    }

    /// <summary>Constructs a Float3 vector from a Vector4.</summary>
    /// <param name="v">Vector3 to convert to Float3</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float3(Vector3 vector) {
      value = Vector128.Create(vector.X, vector.Y, vector.Z, 0);
    }

    /// <summary>Implicitly converts a single half value to a Float3 vector by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to Float3</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float3(half v) { return new Float3(v); }

    /// <summary>Constructs a Float3 vector from a Vector3.</summary>
    /// <param name="v">Vector4 to convert to Float3</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float3(Vector3 v) { return new Float3(v); }

    /// <summary>Constructs a Float3 vector from a Vector3.</summary>
    /// <param name="v">Vector4 to convert to Float3</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector3(Float3 v) { return new Vector3(v.x, v.y, v.z); }
  }
}
