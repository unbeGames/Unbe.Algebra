using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static class Vector256Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Reminder(in Vector256<double> left, in Vector256<double> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Reminder(in Vector256<long> left, in Vector256<long> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> Reminder(in Vector256<ulong> left, in Vector256<ulong> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(in Vector256<double> vector, byte control) {
      if (Avx2.IsSupported) {
        return Avx2.Permute4x64(vector, control);
      }

      return Shuffle(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(in Vector256<double> left, in Vector256<double> right, byte control) {
      if (Avx.IsSupported) {
        return Avx.Shuffle(left, right, control);
      }

      return ShuffleSoftware(left, right, control);

      static Vector256<double> ShuffleSoftware(in Vector256<double> left, in Vector256<double> right, byte control) {
        const byte e0Mask = 0b_0000_0011, e1Mask = 0b_0000_1100, e2Mask = 0b_0011_0000, e3Mask = 0b_1100_0000;

        int e0Selector = control & e0Mask;
        double e0 = left.GetElement(e0Selector);

        int e1Selector = (control & e1Mask) >> 2;
        double e1 = left.GetElement(e1Selector);

        int e2Selector = (control & e2Mask) >> 4;
        double e2 = right.GetElement(e2Selector);

        int e3Selector = (control & e3Mask) >> 6;
        double e3 = right.GetElement(e3Selector);

        return Vector256.Create(e0, e1, e2, e3);
      }
    }

    public static Vector256<double> Truncate(this Vector256<double> vector) {
      if (Avx.IsSupported) {
        return Avx.RoundToZero(vector);
      }

      if (Sse41.IsSupported) {
        Vector128Ext.GetLowHigh(vector, out var low, out var high);
        return FromLowHigh(Sse41.RoundToZero(low), Sse41.RoundToZero(high));
      }

      return SoftwareFallback(vector);

      static Vector256<double> SoftwareFallback(in Vector256<double> vector) {
        return Vector256.Create(
            System.Math.Truncate(vector[0]),
            System.Math.Truncate(vector[1]),
            System.Math.Truncate(vector[2]),
            System.Math.Truncate(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> FromLowHigh<T>(in Vector128<T> low, in Vector128<T> high) where T : struct {
      return low.ToVector256Unsafe().WithUpper(high);
    }

    /// <summary>
    /// Calculates exponentials for each component of Vector256<double> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Exp(in Vector256<double> x) {
      var xx = Avx.Multiply(x, ExpConsts.LOG2EF);
      return TwoToThePowerOf(xx);
    }

    /// <summary>
    /// Calculates 2 ^ x for each component of Vector256<double>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> TwoToThePowerOf(in Vector256<double> v) {
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
      fx = Avx2.ShiftLeftLogical(Avx2.Add(Vector256.AsInt64(fx), ExpConsts.ONE_THOUSAND_TWENTY_THREE), 52).AsDouble();
      y = Avx.Multiply(fx, y);

      // Checks if x is greater than the highest acceptable argument, and sets to infinity if so.
      y = IfElse(Avx.CompareGreaterThanOrEqual(v, ExpConsts.THIGH), ExpConsts.POSITIVE_INFINITY, y);

      // Avx.CompareNotEqual(x, x) is a hack to determine which values of x are NaN, since NaN is the only
      // value that doesn't equal itself. 
      return IfElse(Avx.CompareNotEqual(v, v), ExpConsts.NAN, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Vector256<double> IfElse(Vector256<double> mask, Vector256<double> trueval, Vector256<double> falseval) {
      return Avx.BlendVariable(falseval, trueval, mask);
    }

    internal static class ExpConsts {
      public static readonly Vector256<long> ONE_THOUSAND_TWENTY_THREE = Vector256.Create(0x3ffL);
      public static readonly Vector256<double> HIGH = Vector256.Create(709.0);
      public static readonly Vector256<double> POSITIVE_INFINITY = Vector256.Create(double.PositiveInfinity);
      public static readonly Vector256<double> LOW = Vector256.Create(-709.0);
      public static readonly Vector256<double> LOG2EF = Vector256.Create(1.4426950408889634);
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
