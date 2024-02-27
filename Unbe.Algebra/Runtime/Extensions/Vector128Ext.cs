using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static class Vector128Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> Reminder(in Vector128<int> left, in Vector128<int> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> Reminder(in Vector128<uint> left, in Vector128<uint> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Reminder(in Vector128<float> left, Vector128<float> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    public static Vector128<float> Truncate(this Vector128<float> vector) {
      if (Sse41.IsSupported) {
        return Sse41.RoundToZero(vector);
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(in Vector128<float> vector) {
        return Vector128.Create(
            (float)System.Math.Truncate(vector[0]),
            (float)System.Math.Truncate(vector[1]),
            (float)System.Math.Truncate(vector[2]),
            (float)System.Math.Truncate(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> Shuffle(in Vector128<int> vector, byte control) {
      if (Sse2.IsSupported) {        
        return Sse2.Shuffle(vector, control);
      }

      return ShuffleSoftware(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> Shuffle(in Vector128<uint> vector, byte control) {
      if (Sse2.IsSupported) {
        return Sse2.Shuffle(vector, control);
      }

      return ShuffleSoftware(vector, vector, control);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Shuffle(in Vector128<float> vector, byte control) {
      if (Avx.IsSupported) {
        return Avx.Permute(vector, control);
      }

      return Shuffle(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Shuffle(in Vector128<float> left, in Vector128<float> right, byte control) {
      if (Sse.IsSupported) {
        return Sse.Shuffle(left, right, control);
      }

      return ShuffleSoftware(left, right, control);
    }

    public static void GetLowHigh<T>(in Vector256<T> vector, out Vector128<T> low, out Vector128<T> high) where T : struct {
      low = vector.GetLower();
      high = vector.GetUpper();
    }

    internal static Vector128<T> ShuffleSoftware<T>(Vector128<T> left, Vector128<T> right, byte control) where T : unmanaged {
      const byte e0Mask = 0b_0000_0011, e1Mask = 0b_0000_1100, e2Mask = 0b_0011_0000, e3Mask = 0b_1100_0000;

      int e0Selector = control & e0Mask;
      T e0 = left.GetElement(e0Selector);

      int e1Selector = (control & e1Mask) >> 2;
      T e1 = left.GetElement(e1Selector);

      int e2Selector = (control & e2Mask) >> 4;
      T e2 = right.GetElement(e2Selector);

      int e3Selector = (control & e3Mask) >> 6;
      T e3 = right.GetElement(e3Selector);

      ReadOnlySpan<T> array = stackalloc T[] { e0, e1, e2, e3 };

      return Vector128.Create<T>(array);
    }

    /// <summary>
    /// Calculates exponentials for each component of Vector128<float> using intrinsics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Exp(in Vector128<float> x) {
      var xx = Sse.Multiply(x, ExpConsts.LOG2EF);
      return TwoToThePowerOf(xx);
    }

    /// <summary>
    /// Calculates 2 ^ x for each component of Vector128<float> using intrinsics.
    /// </summary>
    public static Vector128<float> TwoToThePowerOf(in Vector128<float> v) {
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
      fx = Vector128.AsSingle(Sse2.ShiftLeftLogical(Sse2.Add(Sse2.ConvertToVector128Int32(fx), ExpConsts.ONE_HUNDRED_TWENTY_SEVEN), 23));

      // Combines the two exponentials and the end adjustments into the result.
      return Fma.MultiplyAdd(y, fx, end);
    }

    public static class ExpConsts {
      public static readonly Vector128<int> ONE_HUNDRED_TWENTY_SEVEN = Vector128.Create(127);
      public static readonly Vector128<float> HIGH = Vector128.Create(88.3762626647949f);
      public static readonly Vector128<float> POSITIVE_INFINITY = Vector128.Create(float.PositiveInfinity);
      public static readonly Vector128<float> LOW = Vector128.Create(-88.3762626647949f);
      public static readonly Vector128<float> LOG2EF = Vector128.Create(1.4426950408889634f);
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
}
