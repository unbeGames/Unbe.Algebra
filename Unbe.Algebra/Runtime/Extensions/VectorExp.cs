using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector64Ext {
    /// <summary>
    /// Calculates base-e exponential for each component of Vector64<float> using software fallback.
    /// </summary>
    public static Vector64<float> Exp(in Vector64<float> v) {
      return Vector64.Create(
          MathF.Exp(v[0]),
          MathF.Exp(v[1])
      );
    }

    /// <summary>
    /// Calculates base-2 exponential for each component of Vector64<float> using software fallback.
    /// </summary>
    public static Vector64<float> Exp2(in Vector64<float> v) {
      var xx = v * ExpConsts.INVERSE_LOG2EF;
      return Vector64.Create(
          MathF.Exp(xx[0]),
          MathF.Exp(xx[1])
      );
    }

    /// <summary>
    /// Calculates base-10 exponential for each component of Vector64<float> using software fallback.
    /// </summary>
    public static Vector64<float> Exp10(in Vector64<float> v) {
      var xx = v * ExpConsts.EF2LOG10;
      return Vector64.Create(
          MathF.Exp(xx[0]),
          MathF.Exp(xx[1])
      );
    }

    internal static class ExpConsts {
      public static readonly Vector64<float> INVERSE_LOG2EF = Vector64.Create(0.693147180559945f);
      public static readonly Vector64<float> EF2LOG10 = Vector64.Create(2.30258509f);
    }
  }

  public static partial class Vector128Ext {
    /// <summary>
    /// Calculates base-e exponential for each component of Vector128<float> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Exp(in Vector128<float> x) {
      var xx = Sse.Multiply(x, ExpConsts.LOG2EF);
      return Exp2(xx);
    }

    /// <summary>
    /// Calculates base-10 exponential for each component of Vector128<float> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Exp10(in Vector128<float> x) {
      var xx = Sse.Multiply(x, ExpConsts.LOG2LOG10);
      return Exp2(xx);
    }

    /// <summary>
    /// Calculates base-2 exponential for each component of Vector128<float> using intrinsics.
    /// </summary>
    public static Vector128<float> Exp2(in Vector128<float> v) {
      // Checks if x is greater than the highest acceptable argument. Stores the information for later to
      // modify the result. If, for example, only x[1] > EXP_HIGH, then end[1] will be infinity, and the rest
      // zero. We add this to the result at the end, which will force y[1] to be infinity.
      var end = Sse.And(Sse.CompareGreaterThanOrEqual(v, ExpConsts.HIGH), ExpConsts.POSITIVE_INFINITY);

      // Bound x by the maximum and minimum values this algorithm will handle.
      var xx = Sse.Max(Sse.Min(v, ExpConsts.THIGH), ExpConsts.TLOW);

      // Avx.CompareNotEqual(x, x) is a hack to determine which values of x are NaN, since NaN is the only
      // value that doesn't equal itself. If any are NaN, we make the corresponding element of 'end' NaN, and
      // it acts like the infinity adjustment.
      end = Sse.Add(Sse.CompareNotEqual(v, v), end);

      var fx = Sse41.RoundToNearestInteger(xx);

      // This section gets a series approximation for exp(g) in (-0.5, 0.5) since that is g's range.
      xx = Sse.Subtract(xx, fx);

      var y = Fma.MultiplyAdd(ExpConsts.T7, xx, ExpConsts.T6);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T5);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T4);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T3);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T2);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T1);
      y = Fma.MultiplyAdd(y, xx, ExpConsts.T0);

      // Converts n to 2^n. There is no Avx2.ConvertToVector256Int64(fx) intrinsic, so we convert to int32's,
      // since the exponent of a double will never be more than a max int32, then from int to long.
      fx = Vector128.AsSingle(Sse2.ShiftLeftLogical(Sse2.Add(Sse2.ConvertToVector128Int32(fx), Int.ONE_HUNDRED_TWENTY_SEVEN), 23));

      // Combines the two exponentials and the end adjustments into the result.
      return Fma.MultiplyAdd(y, fx, end);
    }

    internal static class ExpConsts {
      public static readonly Vector128<float> HIGH = Vector128.Create(88.3762626647949f);
      public static readonly Vector128<float> POSITIVE_INFINITY = Vector128.Create(float.PositiveInfinity);
      public static readonly Vector128<float> LOW = Vector128.Create(-88.3762626647949f);
      public static readonly Vector128<float> LOG2EF = Vector128.Create(1.4426950408889634f); // log2(e)
      public static readonly Vector128<float> LOG2LOG10 = Vector128.Create(3.3219283f); // log2(10)
      public static readonly Vector128<float> INVERSE_LOG2EF = Vector128.Create(0.693147180559945f);
      public static readonly Vector128<float> P1 = Vector128.Create(1.3981999507E-3f);
      public static readonly Vector128<float> P2 = Vector128.Create(8.3334519073E-3f);
      public static readonly Vector128<float> P3 = Vector128.Create(4.1665795894E-2f);
      public static readonly Vector128<float> P4 = Vector128.Create(1.6666665459E-1f);
      public static readonly Vector128<float> P5 = Vector128.Create(5.0000001201E-1f);
      public static readonly Vector128<float> ONE = Vector128.Create(1.0f);
      public static readonly Vector128<float> THIGH = Vector128.Create(81.0f * 1.4426950408889634f);
      public static readonly Vector128<float> TLOW = Vector128.Create(-81.0f * 1.4426950408889634f);
      public static readonly Vector128<float> T0 = Vector128.Create(1.0f);
      public static readonly Vector128<float> T1 = Vector128.Create(0.6931471805500692f);
      public static readonly Vector128<float> T2 = Vector128.Create(0.240226509999339f);
      public static readonly Vector128<float> T3 = Vector128.Create(0.05550410925060949f);
      public static readonly Vector128<float> T4 = Vector128.Create(0.00961804886829518f);
      public static readonly Vector128<float> T5 = Vector128.Create(0.0013333465742372899f);
      public static readonly Vector128<float> T6 = Vector128.Create(0.000154631026827329f);
      public static readonly Vector128<float> T7 = Vector128.Create(1.530610536076361E-05f);
    }
  }

  public static partial class Vector256Ext {
    /// <summary>
    /// Calculates base-e exponential for each component of Vector256<double> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Exp(in Vector256<double> x) {
      var xx = Avx.Multiply(x, ExpConsts.LOG2EF);
      return Exp2(xx);
    }

    /// <summary>
    /// Calculates base-10 exponential for each component of Vector256<double> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Exp10(in Vector256<double> x) {
      var xx = Avx.Multiply(x, ExpConsts.LOG2LOG10);
      return Exp2(xx);
    }

    /// <summary>
    /// Calculates base-2 exponential for each component of Vector256<double>  using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Exp2(in Vector256<double> v) {
      // Bound x by the maximum and minimum values this algorithm will handle.
      var xx = Avx.Max(Avx.Min(v, ExpConsts.THIGH), ExpConsts.TLOW);
      var fx = Avx.RoundToNearestInteger(xx);

      // This section gets a series approximation for exp(g) in (-0.5, 0.5) since that is g's range.
      xx = Avx.Subtract(xx, fx);

      var xsq = Avx.Multiply(xx, xx);
      var y = Fma.MultiplyAdd(ExpConsts.T11, xsq, ExpConsts.T9);

      var yo = Fma.MultiplyAdd(ExpConsts.T10, xsq, ExpConsts.T8);
      y = Fma.MultiplyAdd(y, xsq, ExpConsts.T7);
      yo = Fma.MultiplyAdd(yo, xsq, ExpConsts.T6);
      y = Fma.MultiplyAdd(y, xsq, ExpConsts.T5);
      yo = Fma.MultiplyAdd(yo, xsq, ExpConsts.T4);
      y = Fma.MultiplyAdd(y, xsq, ExpConsts.T3);
      yo = Fma.MultiplyAdd(yo, xsq, ExpConsts.T2);
      y = Fma.MultiplyAdd(y, xsq, ExpConsts.T1);
      yo = Fma.MultiplyAdd(yo, xsq, ExpConsts.T0);
      y = Fma.MultiplyAdd(y, xx, yo);

      // Converts n to 2^n. There is no Avx2.ConvertToVector256Int64(fx) intrinsic, so we convert to int32's,
      // since the exponent of a double will never be more than a max int32, then from int to long.
      fx = Avx.Add(fx, ExpConsts.MAGIC_LONG_DOUBLE_ADD);
      fx = Avx2.ShiftLeftLogical(Avx2.Add(Vector256.AsInt64(fx), Long.ONE_THOUSAND_TWENTY_THREE), 52).AsDouble();
      y = Avx.Multiply(fx, y);

      // Checks if x is greater than the highest acceptable argument, and sets to infinity if so.
      y = IfElse(Avx.CompareGreaterThanOrEqual(v, ExpConsts.THIGH), ExpConsts.POSITIVE_INFINITY, y);

      // Avx.CompareNotEqual(x, x) is a hack to determine which values of x are NaN, since NaN is the only
      // value that doesn't equal itself. 
      return IfElse(Avx.CompareNotEqual(v, v), ExpConsts.NAN, y);
    }

    internal static class ExpConsts { 
      public static readonly Vector256<double> HIGH = Vector256.Create(709.0);
      public static readonly Vector256<double> POSITIVE_INFINITY = Vector256.Create(double.PositiveInfinity);
      public static readonly Vector256<double> LOW = Vector256.Create(-709.0);
      public static readonly Vector256<double> LOG2EF = Vector256.Create(1.4426950408889634);
      public static readonly Vector256<double> LOG2LOG10 = Vector256.Create(3.321928094887362); // log2(10)
      public static readonly Vector256<double> INVERSE_LOG2EF = Vector256.Create(0.693147180559945);
      public static readonly Vector256<double> ONE = Vector256.Create(1.0);
      public static readonly Vector256<double> MAGIC_LONG_DOUBLE_ADD = Vector256.Create(6755399441055744.0);
      public static readonly Vector256<double> INVE = Vector256.Create(0.367879441171442321595523770161);
      public static readonly Vector256<double> THIGH = Vector256.Create(709.0 * 1.4426950408889634);
      public static readonly Vector256<double> TLOW = Vector256.Create(-709.0 * 1.4426950408889634);
      public static readonly Vector256<double> NAN = Vector256.Create(double.NaN);
      public static readonly Vector256<double> T0 = Vector256.Create(1.0);
      public static readonly Vector256<double> T1 = Vector256.Create(0.6931471805599453087156032);
      public static readonly Vector256<double> T2 = Vector256.Create(0.240226506959101195979507231);
      public static readonly Vector256<double> T3 = Vector256.Create(0.05550410866482166557484);
      public static readonly Vector256<double> T4 = Vector256.Create(0.00961812910759946061829085);
      public static readonly Vector256<double> T5 = Vector256.Create(0.0013333558146398846396);
      public static readonly Vector256<double> T6 = Vector256.Create(0.0001540353044975008196326);
      public static readonly Vector256<double> T7 = Vector256.Create(0.000015252733847608224);
      public static readonly Vector256<double> T8 = Vector256.Create(0.000001321543919937730177);
      public static readonly Vector256<double> T9 = Vector256.Create(0.00000010178055034703);
      public static readonly Vector256<double> T10 = Vector256.Create(0.000000007073075504998510);
      public static readonly Vector256<double> T11 = Vector256.Create(0.00000000044560630323);
    }
  }
}
