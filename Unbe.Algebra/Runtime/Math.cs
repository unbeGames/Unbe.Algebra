using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

#pragma warning disable 8981, IDE1006

namespace Unbe.Algebra { 
  public static partial class Math {
    public const int TRUE = -1;
    public const int FALSE = 0;

    /// <summary> /// The conversion constant used to convert radians to degrees. Multiply the radian value by this constant to get degrees.</summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.degrees(double)"/>.</remarks>
    public const double TO_DEGREES_DBL = 57.29577951308232;

    /// <summary> The conversion constant used to convert degrees to radians. Multiply the degree value by this constant to get radians.</summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(double)"/>.</remarks>
    public const double TO_RADIANS_DBL = 0.017453292519943296;

    /// <summary>The mathematical constant pi. Approximately 3.14. This is a double precision constant.</summary>
    public const double PI_DBL = System.Math.PI;

    /// <summary> The mathematical constant (2 * pi). Approximately 6.28. This is a double precision constant. Also known as <see cref="TAU_DBL"/>.</summary>
    public const double PI2_DBL = System.Math.Tau;

    /// <summary> The mathematical constant (pi / 2). Approximately 1.57. This is a double precision constant. </summary>
    public const double PI_HALF_DBL = PI_DBL * 0.5;

    /// <summary>The mathematical constant e also known as Euler's number. Approximately 2.72. This is a double precision constant.</summary>
    public const double E_DBL = System.Math.E;

    /// <summary>The square root 2. Approximately 1.41. This is a double precision constant.</summary>
    public const double SQRT2_DBL = 1.41421356237309504880;


    /// <summary> The conversion constant used to convert radians to degrees. Multiply the radian value by this constant to get degrees.</summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(float)" />.</ remarks >
    public const float TO_DEGREES = (float)TO_DEGREES_DBL;

    /// <summary> The conversion constant used to convert degrees to radians. Multiply the degree value by this constant to get radians.</summary>
    /// <remarks>Multiplying by this constant is equivalent to using <see cref = "Math.radians(float)"/>.</remarks>
    public const float TO_RADIANS = (float)TO_RADIANS_DBL;


    /// <summary>The mathematical constant pi. Approximately 3.14.</summary>
    public const float PI = MathF.PI;

    /// <summary> The mathematical constant (2 * pi). Approximately 6.28. Also known as <see cref="TAU"/>.</summary>
    public const float PI2 = MathF.Tau;

    /// <summary> The mathematical constant (pi / 2). Approximately 1.57. </summary>
    public const float PI_HALF = (float)PI_HALF_DBL;

    /// <summary>The mathematical constant e also known as Euler's number. Approximately 2.72.</summary>
    public const float E = MathF.E;

    /// <summary>The square root 2. Approximately 1.41.</summary>
    public const float SQRT2 = (float)SQRT2_DBL;


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


    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte clamp(byte value, byte lower, byte upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short clamp(short value, short lower, short upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort clamp(ushort value, ushort lower, ushort upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int clamp(int value, int lower, int upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint clamp(uint value, uint lower, uint upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long clamp(long value, long lower, long upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong clamp(ulong value, ulong lower, ulong upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float clamp(float value, float lower, float upper) { return max(lower, min(upper, value)); }

    /// <summary>Returns the result of clamping the vector into the interval (inclusive) [lower, upper], where value, lower and upper are vectors of the same type.</summary>
    /// <param name="value">Input value to be clamped.</param>
    /// <param name="lower">Lower bound of the interval.</param>
    /// <param name="upper">Upper bound of the interval.</param>
    /// <returns>The clamping of the input value into the interval (inclusive) [lower, upper].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double clamp(double value, double lower, double upper) { return max(lower, min(upper, value)); }


    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte saturate(byte x) { return clamp(x, (byte)0, (byte)1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short saturate(short x) { return clamp(x, (short)0, (short)1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort saturate(ushort x) { return clamp(x, (ushort)0, (ushort)1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int saturate(int x) { return clamp(x, 0, 1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint saturate(uint x) { return clamp(x, 0, 1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long saturate(long x) { return clamp(x, 0, 1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong saturate(ulong x) { return clamp(x, 0, 1); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float saturate(float x) { return clamp(x, 0.0f, 1.0f); }

    /// <summary>Returns the result of clamping the value x into the interval [0, 1].</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The clamping of the input into the interval [0, 1].</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double saturate(double x) { return clamp(x, 0.0, 1.0); }

    #endregion


    #region Integral Numerics Math

    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short ceilpow2(short x) {
      int y = x - 1;
      y |= y >> 1;
      y |= y >> 2;
      y |= y >> 4;
      y |= y >> 8;
      return (short)(y + 1);
    }

    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ceilpow2(ushort x) {
      int y = x - 1;
      y |= y >> 1;
      y |= y >> 2;
      y |= y >> 4;
      y |= y >> 8;
      return (ushort)(y + 1);
    }


    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ceilpow2(int x) {
      x -= 1;
      x |= x >> 1;
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      return x + 1;
    }

    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ceilpow2(uint x) {
      x -= 1;
      x |= x >> 1;
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      return x + 1;
    }

    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ceilpow2(long x) {
      x -= 1;
      x |= x >> 1;
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      x |= x >> 32;
      return x + 1;
    }


    /// <summary>Returns the smallest power of two greater than or equal to the input.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The smallest power of two greater than or equal to the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ceilpow2(ulong x) {
      x -= 1;
      x |= x >> 1;
      x |= x >> 2;
      x |= x >> 4;
      x |= x >> 8;
      x |= x >> 16;
      x |= x >> 32;
      return x + 1;
    }

    #endregion


    #region Sign Math

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


    /// <summary>Returns the absolute value of a short value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The absolute value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short abs(short x) { return System.Math.Abs(x); }

    /// <summary>Returns the absolute value of a int value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The absolute value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int abs(int x) { return System.Math.Abs(x); }

    /// <summary>Returns the absolute value of a long value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The absolute value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long abs(long x) { return System.Math.Abs(x); }

    /// <summary>Returns the absolute value of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The absolute value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float abs(float x) { return System.Math.Abs(x); }

    /// <summary>Returns the absolute value of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The absolute value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double abs(double x) { return System.Math.Abs(x); }

    #endregion


    #region Floating Point Math

    /// <summary>Returns the result of rounding a float value up to the nearest integral value less or equal to the original value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round down to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float floor(float x) { return System.MathF.Floor(x); }

    /// <summary>Returns the result of rounding a double value up to the nearest integral value less or equal to the original value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round down to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double floor(double x) { return System.Math.Floor(x); }


    /// <summary>Returns the result of rounding a float value up to the nearest integral value greater or equal to the original value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round up to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ceil(float x) { return System.MathF.Ceiling(x); }

    /// <summary>Returns the result of rounding a double value up to the nearest greater integral value greater or equal to the original value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round up to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ceil(double x) { return System.Math.Ceiling(x); }


    /// <summary>Returns the result of rounding a float value to the nearest integral value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float round(float x) { return MathF.Round(x); }

    /// <summary>Returns the result of rounding a double value to the nearest integral value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The round to nearest integral value of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double round(double x) { return System.Math.Round(x); }


    /// <summary>Returns the base-e exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-e exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float exp(float x) { return MathF.Exp(x); }

    /// <summary>Returns the base-e exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-e exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double exp(double x) { return System.Math.Exp(x); }


    /// <summary>Returns the base-2 exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-2 exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double exp2(double x) { return System.Math.Exp(x * 0.693147180559945309); }

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

    /// <summary>Returns the base-10 exponential of x.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-10 exponential of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double exp10(double x) { return System.Math.Exp(x * 2.302585092994045684); }


    /// <summary>Returns the natural logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The natural logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log(float x) { return MathF.Log(x); }

    /// <summary>Returns the natural logarithm of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The natural logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double log(double x) { return System.Math.Log(x); }


    /// <summary>Returns the base-2 logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-2 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log2(float x) { return MathF.Log(x, 2.0f); }

    /// <summary>Returns the base-2 logarithm of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-2 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double log2(double x) { return System.Math.Log(x, 2.0); }


    /// <summary>Returns the base-10 logarithm of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-10 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float log10(float x) { return MathF.Log10(x); }

    /// <summary>Returns the base-10 logarithm of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The base-10 logarithm of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double log10(double x) { return System.Math.Log10(x); }


    /// <summary>Returns x raised to the power y.</summary>
    /// <param name="x">The exponent base.</param>
    /// <param name="y">The exponent power.</param>
    /// <returns>The result of raising x to the power y.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float pow(float x, float y) { return MathF.Pow(x, y); }

    /// <summary>Returns x raised to the power y.</summary>
    /// <param name="x">The exponent base.</param>
    /// <param name="y">The exponent power.</param>
    /// <returns>The result of raising x to the power y.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double pow(double x, double y) { return System.Math.Pow(x, y); }


    /// <summary>Returns the square root of a float value.</summary>
    /// <param name="x">Value to use when computing square root.</param>
    /// <returns>The square root.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float sqrt(float x) { return MathF.Sqrt(x); }

    /// <summary>Returns the square root of a double value.</summary>
    /// <param name="x">Value to use when computing square root.</param>
    /// <returns>The square root.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double sqrt(double x) { return System.Math.Sqrt(x); }


    /// <summary>Returns the reciprocal square root of a float value.</summary>
    /// <param name="x">Value to use when computing reciprocal square root.</param>
    /// <returns>The reciprocal square root.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float rsqrt(float x) { return 1.0f / MathF.Sqrt(x); }

    /// <summary>Returns the reciprocal square root of a double value.</summary>
    /// <param name="x">Value to use when computing reciprocal square root.</param>
    /// <returns>The reciprocal square root.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double rsqrt(double x) { return 1.0 / System.Math.Sqrt(x); }
    
    #endregion


    #region Trigonometry

    /// <summary>Returns the sine of a float value.</summary>
    /// <param name="x">Input value in radians.</param>
    /// <returns>The sine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float sin(float x) { return MathF.Sin(x); }

    /// <summary>Returns the sine of a double value.</summary>
    /// <param name="x">Input value in radians.</param>
    /// <returns>The sine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double sin(double x) { return System.Math.Sin(x); }

    /// <summary>Returns the cosine of a float value.</summary>
    /// <param name="x">Input value in radians.</param>
    /// <returns>The cosine cosine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float cos(float x) { return System.MathF.Cos(x); }

    /// <summary>Returns the cosine of a double value.</summary>
    /// <param name="x">Input value in radians.</param>
    /// <returns>The cosine cosine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double cos(double x) { return System.Math.Cos(x); }

    /// <summary>Returns the sine and cosine of the input float value x through the out parameters s and c.</summary>
    /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
    /// <param name="x">Input angle in radians.</param>
    /// <param name="s">Output sine of the input.</param>
    /// <param name="c">Output cosine of the input.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void sincos(float x, out float s, out float c) { s = sin(x); c = cos(x); }

    /// <summary>Returns the sine and cosine of the input double value x through the out parameters s and c.</summary>
    /// <remarks>When Burst compiled, his method is faster than calling sin() and cos() separately.</remarks>
    /// <param name="x">Input angle in radians.</param>
    /// <param name="s">Output sine of the input.</param>
    /// <param name="c">Output cosine of the input.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void sincos(double x, out double s, out double c) { s = sin(x); c = cos(x); }

    /// <summary>Returns the arcsine of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The arcsine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float asin(float x) { return MathF.Asin((float)x); }

    /// <summary>Returns the arcsine of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The arcsine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double asin(double x) { return System.Math.Asin(x); }

    /// <summary>Returns the arccosine of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The arccosine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float acos(float x) { return MathF.Acos(x); }

    /// <summary>Returns the arccosine of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The arccosine of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double acos(double x) { return System.Math.Acos(x); }

    /// <summary>Returns the tangent of a float value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The tangent of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float tan(float x) { return (float)System.Math.Tan(x); }

    /// <summary>Returns the tangent of a double value.</summary>
    /// <param name="x">Input value.</param>
    /// <returns>The tangent of the input.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double tan(double x) { return System.Math.Tan(x); }

    /// <summary>Returns the arctangent of a float value.</summary>
    /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
    /// <returns>The arctangent of the input, in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float atan(float x) { return MathF.Atan(x); }

    /// <summary>Returns the arctangent of a double value.</summary>
    /// <param name="x">A tangent value, usually the ratio y/x on the unit circle.</param>
    /// <returns>The arctangent of the input, in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double atan(double x) { return System.Math.Atan(x); }

    /// <summary>Returns the 2-argument arctangent of a pair of float values.</summary>
    /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
    /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
    /// <returns>The arctangent of the ratio y/x, in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float atan2(float y, float x) { return MathF.Atan2(y, x); }

    /// <summary>Returns the 2-argument arctangent of a pair of double values.</summary>
    /// <param name="y">Numerator of the ratio y/x, usually the y component on the unit circle.</param>
    /// <param name="x">Denominator of the ratio y/x, usually the x component on the unit circle.</param>
    /// <returns>The arctangent of the ratio y/x, in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double atan2(double y, double x) { return System.Math.Atan2(y, x); }

    #endregion


    #region Vector Operations

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte dot(byte x, byte y) { return (byte)(x * y); }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short dot(short x, short y) { return (short)(x * y); }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort dot(ushort x, ushort y) { return (ushort)(x * y); }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int dot(int x, int y) { return x * y; }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint dot(uint x, uint y) { return x * y; }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long dot(long x, long y) { return x * y; }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong dot(ulong x, ulong y) { return x * y; }

    /// <summary>Returns the dot product of two values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float dot(float x, float y) { return x * y; }

    /// <summary>Returns the dot product of two double values. Equivalent to multiplication.</summary>
    /// <param name="x">The first value.</param>
    /// <param name="y">The second value.</param>
    /// <returns>The dot product of two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double dot(double x, double y) { return x * y; }

    #endregion


    #region Boolean Math

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short select(short falseValue, short trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort select(ushort falseValue, ushort trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int select(int falseValue, int trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint select(uint falseValue, uint trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long select(long falseValue, long trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong select(ulong falseValue, ulong trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float select(float falseValue, float trueValue, bool test) { return test ? trueValue : falseValue; }

    /// <summary>The selection between falseValue and trueValue according to bool test.</summary>
    /// <param name="falseValue">Value to use if test is false.</param>
    /// <param name="trueValue">Value to use if test is true.</param>
    /// <param name="test">Bool value to choose between falseValue and trueValue.</param>
    /// <returns>Returns trueValue if test is true, falseValue otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double select(double falseValue, double trueValue, bool test) { return test ? trueValue : falseValue; }

    #endregion
  }
}
