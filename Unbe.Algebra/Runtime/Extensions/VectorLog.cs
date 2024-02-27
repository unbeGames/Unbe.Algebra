using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector64Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Log(in Vector64<float> v) {
      return Vector64.Create(
        (float)System.Math.Log(v[0]),
        (float)System.Math.Log(v[1])
      );
    }

    public static Vector64<float> Log2(in Vector64<float> v) {
      return Vector64.Create(
        (float)System.Math.Log2(v[0]),
        (float)System.Math.Log2(v[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Log10(in Vector64<float> v) {
      return Vector64.Create(
        (float)System.Math.Log10(v[0]),
        (float)System.Math.Log10(v[1])
      );
    }
  }

  public static partial class Vector128Ext {
    /// <summary>
    /// Computes the natural log on each component of Vector128<float> using intrinsics. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Log(Vector128<float> v) {
      var log2 = Log2(v);
      return Sse.Multiply(LogConsts.LN2, log2);
    }

    /// <summary>
    /// Computes log base 10's on each component of Vector128<float> using intrinsics. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Log10(Vector128<float> v) {
      var log2 = Log2(v);
      return Sse.Multiply(LogConsts.LOG10, log2);
    }

    /// <summary>
    /// Computes log base 2's on each component of Vector128<float> using intrinsics. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Log2(in Vector128<float> x) {
      var end = Sse.CompareLessThanOrEqual(x, LogConsts.ZERO);
      end = Sse.Add(Sse.And(Avx.CompareEqual(x, LogConsts.POSITIVE_INFINITY), LogConsts.POSITIVE_INFINITY), end);
      end = Sse.Add(Sse.CompareNotEqual(x, x), end);

      var xl = Vector128.AsInt32(Sse.Max(x, LogConsts.ZERO));
      var m = Sse2.Subtract(Sse2.ShiftRightLogical(xl, 23), ONE_HUNDRED_TWENTY_SEVEN);

      var y = Sse2.ConvertToVector128Single(m);

      xl = Sse2.Or(Sse2.And(xl, DECIMAL_MASK_FOR_FLOAT), EXPONENT_MASK_FOR_FLOAT);

      var d = Sse.Multiply(Sse.Or(Vector128.AsSingle(xl), LogConsts.ONE), LogConsts.TWO_THIRDS);

      LogApprox(in d, ref d);

      y = Sse.Add(Sse.Add(Sse.Multiply(d, LogConsts.LOG2EF), LogConsts.LOG_ONE_POINT_FIVE), y);
      return Sse.Add(end, y);
    }

    /// <summary>
    /// A Taylor Series approximation of ln(x) that relies on the identity that ln(x) = 2*atan((x-1)/(x+1)).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void LogApprox(in Vector128<float> x, ref Vector128<float> y) {
      y = Sse.Divide(Sse.Subtract(x, LogConsts.ONE), Sse.Add(x, LogConsts.ONE));
      var ysq = Sse.Multiply(y, y);

      var rx = Sse.Multiply(ysq, LogConsts.ONE_ELEVENTH);
      rx = Sse.Add(rx, LogConsts.ONE_NINTH);
      rx = Sse.Multiply(ysq, rx);
      rx = Sse.Add(rx, LogConsts.ONE_SEVENTH);
      rx = Sse.Multiply(ysq, rx);
      rx = Sse.Add(rx, LogConsts.ONE_FIFTH);
      rx = Sse.Multiply(ysq, rx);
      rx = Sse.Add(rx, LogConsts.ONE_THIRD);
      rx = Sse.Multiply(ysq, rx);
      rx = Sse.Add(rx, LogConsts.ONE);
      rx = Sse.Multiply(y, rx);
      y = Sse.Multiply(rx, LogConsts.TWO);
    }

    public static class LogConsts {
      public static readonly Vector128<float> ZERO = Vector128<float>.Zero;
      public static readonly Vector128<float> POSITIVE_INFINITY = Vector128.Create(float.PositiveInfinity);
      public static readonly Vector128<float> LOG2EF = Vector128.Create(1.4426950408889634f);
      public static readonly Vector128<float> TWO_THIRDS = Vector128.Create(0.66666666666666666f);
      public static readonly Vector128<float> LOG_ONE_POINT_FIVE = Vector128.Create(0.58496250072115619f);
      public static readonly Vector128<float> ONE_THIRD = Vector128.Create(1.0f / 3.0f);
      public static readonly Vector128<float> ONE_FIFTH = Vector128.Create(1.0f / 5.0f);
      public static readonly Vector128<float> ONE_SEVENTH = Vector128.Create(1.0f / 7.0f);
      public static readonly Vector128<float> ONE_NINTH = Vector128.Create(1.0f / 9.0f);
      public static readonly Vector128<float> ONE_ELEVENTH = Vector128.Create(1.0f / 11.0f);
      public static readonly Vector128<float> ONE = Vector128.Create(1.0f);
      public static readonly Vector128<float> LN2 = Vector128.Create(0.69314718055994530941f);
      public static readonly Vector128<float> LOG10 = Vector128.Create(0.30102999566398115676f);
      public static readonly Vector128<float> TWO = Vector128.Create(2.0f);
    }
  }

  public static partial class Vector256Ext {
    /// <summary>
    /// Computes the natural log on each component of Vector256<double> using intrinsics. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Log(in Vector256<double> v) {     
      var log2 = Log2(v);
      return Avx.Multiply(LogConsts.LN2, log2);
    }

    /// <summary>
    /// Computes log base 10's on each component of Vector256<double> using intrinsics. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Log10(in Vector256<double> v) {
      var log2 = Log2(v);
      return Avx.Multiply(LogConsts.LOG10, log2);
    }

    /// <summary>
    /// Computes log base 2's on each component of Vector256<double> using intrinsics. 
    /// </summary>
    /// <param name="x">A reference to the 4 arguments</param>
    /// <param name="y">The 4 results</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Log2(in Vector256<double> x) {
      // This algorithm uses the properties of floating point number to transform x into d*2^m, so log(x)
      // becomes log(d)+m, where d is in [1, 2]. Then it uses a series approximation of log to approximate 
      // the value in [1, 2]

      var xl = Vector256.AsInt64(Avx.Max(x, LogConsts.ZERO));
      var mantissa = Avx2.Subtract(Avx2.ShiftRightLogical(xl, 52), ONE_THOUSAND_TWENTY_THREE);

      var y = Vector256.Create(0.0);
      ConvertLongToDouble(in mantissa, ref y);

      xl = Avx2.Or(Avx2.And(xl, DECIMAL_MASK_FOR_DOUBLE), EXPONENT_MASK_FOR_DOUBLE);

      var d = Avx.Multiply(Avx.Or(Vector256.AsDouble(xl), LogConsts.ONE), LogConsts.TWO_THIRDS);

      LogApprox(in d, ref d);

      y = Avx.Add(Fma.MultiplyAdd(d, LogConsts.LOG2EF, LogConsts.LOG_ONE_POINT_FIVE), y);
      
      y = IfElse(Avx.CompareEqual(x, LogConsts.ZERO), LogConsts.NEGATIVE_INFINITY, y);
      y = IfElse(Avx.CompareEqual(x, LogConsts.POSITIVE_INFINITY), LogConsts.POSITIVE_INFINITY, y);
      return IfElse(Avx.CompareNotEqual(x, x), LogConsts.NAN, y);
    }

    /// <summary>
    /// A Taylor Series approximation of ln(x) that relies on the identity that ln(x) = 2*atan((x-1)/(x+1)).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void LogApprox(in Vector256<double> x, ref Vector256<double> y) {
      y = Avx.Divide(Avx.Subtract(x, LogConsts.ONE), Avx.Add(x, LogConsts.ONE));
      var ysq = Avx.Multiply(y, y);

      var rx = Fma.MultiplyAdd(ysq, LogConsts.L8, LogConsts.L7);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L6);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L5);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L4);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L3);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L2);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.L1);
      rx = Fma.MultiplyAdd(ysq, rx, LogConsts.TWO);

      y = Avx.Multiply(y, rx);
    }

    internal static class LogConsts {
      public static readonly Vector256<double> LOG2EF = Vector256.Create(1.4426950408889634);
      public static readonly Vector256<double> TWO_THIRDS = Vector256.Create(2.0 / 3.0);
      public static readonly Vector256<double> LOG_ONE_POINT_FIVE = Vector256.Create(0.58496250072115619);
      public static readonly Vector256<double> ZERO = Vector256<double>.Zero;
      public static readonly Vector256<double> NAN = Vector256.Create(double.NaN);
      public static readonly Vector256<double> POSITIVE_INFINITY = Vector256.Create(double.PositiveInfinity);
      public static readonly Vector256<double> NEGATIVE_INFINITY = Vector256.Create(double.PositiveInfinity);
      public static readonly Vector256<double> TWO = Vector256.Create(2.0);
      public static readonly Vector256<double> ONE = Vector256.Create(1.0);
      public static readonly Vector256<double> LN2 = Vector256.Create(0.6931471805599453094172321214581766);
      public static readonly Vector256<double> LOG10 = Vector256.Create(0.30102999566398115676272048976704);
      public static readonly Vector256<double> L1 = Vector256.Create(0.6666666666598753418813);
      public static readonly Vector256<double> L2 = Vector256.Create(0.40000000155972106981);
      public static readonly Vector256<double> L3 = Vector256.Create(0.285714152842158938);
      public static readonly Vector256<double> L4 = Vector256.Create(0.22222793889691212);
      public static readonly Vector256<double> L5 = Vector256.Create(0.181678841929303);
      public static readonly Vector256<double> L6 = Vector256.Create(0.15584588873060);
      public static readonly Vector256<double> L7 = Vector256.Create(0.11675575574300);
      public static readonly Vector256<double> L8 = Vector256.Create(0.1889921862784);
    }
  }
}
