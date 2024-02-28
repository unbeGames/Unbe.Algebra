using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

#pragma warning disable 8981, IDE1006

namespace Unbe.Algebra { 
  public static partial class Math {
    /// <summary>
    /// The conversion constant used to convert radians to degrees. Multiply the radian value by this constant to get degrees.
    /// </summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.degrees(double)"/>.</remarks>
    public const double TO_DEGREES_DBL = 57.29577951308232;

    /// <summary>
    /// The conversion constant used to convert degrees to radians. Multiply the degree value by this constant to get radians.
    /// </summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(double)"/>.</remarks>
    public const double TO_RADIANS_DBL = 0.017453292519943296;


    /// <summary>
    /// The conversion constant used to convert radians to degrees. Multiply the radian value by this constant to get degrees.
    /// </summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(float)" />.</ remarks >
    public const float TO_DEGREES = (float)TO_DEGREES_DBL;

    /// <summary>
    /// The conversion constant used to convert degrees to radians. Multiply the degree value by this constant to get radians.
    /// </summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(float)"/>.</remarks>
    public const float TO_RADIANS = (float)TO_RADIANS_DBL;

    /// <summary>Returns the bit pattern of a uint4 as a float4.</summary>
    /// <param name="x">The uint4 bits to copy.</param>
    /// <returns>The float4 with the same bit pattern as the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4 asfloat(UInt4 x) {
      return new Float4(x.value.As<uint, float>());
    }

    #region Degrees/Radians conversion
    /// <summary>Returns the result of converting a float value from degrees to radians.</summary>
    /// <param name="x">Angle in degrees.</param>
    /// <returns>Angle converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float radians(float x) { return x * TO_RADIANS; }

    /// <summary>Returns the result of a componentwise conversion of a Float2 vector from degrees to radians.</summary>
    /// <param name="x">Vector containing angles in degrees.</param>
    /// <returns>Vector containing angles converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float2 radians(Float2 x) { return x * TO_RADIANS; }

    /// <summary>Returns the result of a componentwise conversion of a Float3 vector from degrees to radians.</summary>
    /// <param name="x">Vector containing angles in degrees.</param>
    /// <returns>Vector containing angles converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float3 radians(Float3 x) { return x * TO_RADIANS; }

    /// <summary>Returns the result of a componentwise conversion of a Float4 vector from degrees to radians.</summary>
    /// <param name="x">Vector containing angles in degrees.</param>
    /// <returns>Vector containing angles converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4 radians(Float4 x) { return x * TO_RADIANS; }


    /// <summary>Returns the result of converting a float value from radians to degrees.</summary>
    /// <param name="x">Angle in radians.</param>
    /// <returns>Angle converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float degrees(float x) { return x * TO_DEGREES; }

    /// <summary>Returns the result of a componentwise conversion of a Float2 vector from radians to degrees.</summary>
    /// <param name="x">Vector containing angles in radians.</param>
    /// <returns>Vector containing angles converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float2 degrees(Float2 x) { return x * TO_DEGREES; }

    /// <summary>Returns the result of a componentwise conversion of a Float3 vector from radians to degrees.</summary>
    /// <param name="x">Vector containing angles in radians.</param>
    /// <returns>Vector containing angles converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float3 degrees(Float3 x) { return x * TO_DEGREES; }

    /// <summary>Returns the result of a componentwise conversion of a Float4 vector from radians to degrees.</summary>
    /// <param name="x">Vector containing angles in radians.</param>
    /// <returns>Vector containing angles converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4 degrees(Float4 x) { return x * TO_DEGREES; }
    #endregion

    #region Basic Math
    /// <summary>Returns the base-e exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-e exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float exp(float x) { return MathF.Exp(x); }

    /// <summary>Returns the base-2 exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-2 exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float exp2(float x) { return MathF.Exp(x * 0.69314718f); }

    /// <summary>Returns the base-10 exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-10 exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float exp10(float x) { return MathF.Exp(x * 2.30258509f); }


    /// <summary>Returns the natural logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The natural logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log(float x) { return MathF.Log(x); }

    /// <summary>Returns the base-2 logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-2 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log2(float x) { return MathF.Log(x, 2.0f); }

    /// <summary>Returns the base-10 logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-10 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log10(float x) { return MathF.Log10(x); }
    #endregion
  }
}
