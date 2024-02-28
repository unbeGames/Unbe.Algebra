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
    public static Float4 asfloat(UInt4 x) { return new Float4(x.value.As<uint, float>()); }

    #region Degrees/Radians Conversion
    /// <summary>Returns the result of converting a float value from degrees to radians.</summary>
    /// <param name="x">Angle in degrees.</param>
    /// <returns>Angle converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float radians(float x) { return x * TO_RADIANS; }

    /// <summary>Returns the result of converting a double value from degrees to radians.</summary>
    /// <param name="x">Angle in degrees.</param>
    /// <returns>Angle converted to radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double radians(double x) { return x * TO_RADIANS_DBL; }

    /// <summary>Returns the result of converting a float value from radians to degrees.</summary>
    /// <param name="x">Angle in radians.</param>
    /// <returns>Angle converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float degrees(float x) { return x * TO_DEGREES; }

    /// <summary>Returns the result of converting a double value from radians to degrees.</summary>
    /// <param name="x">Angle in radians.</param>
    /// <returns>Angle converted to degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double degrees(double x) { return x * TO_DEGREES_DBL; }
    #endregion

    #region Core Math
    /// <summary>Returns the minimum of two byte values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte min(byte x, byte y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two short values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short min(short x, short y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two ushort values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort min(ushort x, ushort y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two int values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int min(int x, int y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two uint values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint min(uint x, uint y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two long values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long min(long x, long y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two ulong values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong min(ulong x, ulong y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two float values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float min(float x, float y) { return System.Math.Min(x, y); }

    /// <summary>Returns the minimum of two double values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The minimum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double min(double x, double y) { return System.Math.Min(x, y); }


    /// <summary>Returns the maximum of two byte values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte max(byte x, byte y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two short values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short max(short x, short y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two ushort values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort max(ushort x, ushort y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two int values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int max(int x, int y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two uint values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint max(uint x, uint y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two long values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long max(long x, long y) { return System.Math.Max(x, y); }


    /// <summary>Returns the maximum of two ulong values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong max(ulong x, ulong y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two float values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float max(float x, float y) { return System.Math.Max(x, y); }

    /// <summary>Returns the maximum of two double values.</summary>
    /// <param name="x">The first input value.</param>
    /// <param name="y">The second input value.</param>
    /// <returns>The maximum of the two input values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double max(double x, double y) { return System.Math.Max(x, y); }
    #endregion


    #region Sign
    /// <summary>Returns the sign of a short value. -1 if it is less than zero, 0 if it is zero and 1 if it greater than zero.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The sign of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int sign(short x) { return System.Math.Sign(x); }

    /// <summary>Returns the sign of a int value. -1 if it is less than zero, 0 if it is zero and 1 if it greater than zero.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The sign of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int sign(int x) { return System.Math.Sign(x); }

    /// <summary>Returns the sign of a float value. -1.0f if it is less than zero, 0.0f if it is zero and 1.0f if it greater than zero.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The sign of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float sign(float x) { return MathF.Sign(x); }

    /// <summary>Returns the sign of a long value. -1 if it is less than zero, 0 if it is zero and 1 if it greater than zero.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The sign of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int sign(long x) { return System.Math.Sign(x); }

    /// <summary>Returns the sign of a double value. -1.0 if it is less than zero, 0.0 if it is zero and 1.0 if it greater than zero.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The sign of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double sign(double x) { return System.Math.Sign(x); }
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


    /// <summary>Returns x raised to the power y.</summary>
    /// <param name="x">The exponent base.</param>
    /// <param name="y">The exponent power.</param>
    /// <returns>The result of raising x to the power y.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float pow(float x, float y) { return MathF.Pow(x, y); }
    #endregion
  }
}
