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


    /// <summary>Returns the result of converting a float value from degrees to radians.</summary>
    /// <param name="x">Angle in degrees.</param>
    /// <returns>Angle converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double radians(double x) { return x * TO_RADIANS_DBL; }


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
  }
}
