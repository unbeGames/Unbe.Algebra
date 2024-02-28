using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector256Ext {
    

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Sign(Vector256<double> vector) {
      var v = Vector256.Min(vector, Double.ONE);
      return Vector256.Max(v, Double.NEGATIVE_ONE);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Sign(Vector256<long> vector) {
      var v = Vector256.Min(vector, Long.ONE);
      return Vector256.Max(v, Long.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> ExtractSign(Vector256<double> vector) {
      return vector & Double.MASK_NOT_SIGN;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> ExtractSign(Vector256<long> vector) {
      return vector & Long.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Pow(in Vector256<double> x, in Vector256<double> y) {
      return Exp(y * Log(x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Vector256<double> IfElse(in Vector256<double> mask, in Vector256<double> trueval, in Vector256<double> falseval) {
      return Avx.BlendVariable(falseval, trueval, mask);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void ConvertLongToDouble(in Vector256<long> x, ref Vector256<double> y) {
      y = Avx.Subtract(Vector256.AsDouble(Avx2.Add(x, Long.MAGIC_LONG_ADD)), Double.MAGIC_DOUBLE_ADD);
    }

    public static class Long {
      public static readonly Vector256<long> ONE_THOUSAND_TWENTY_THREE = Vector256.Create(0x3ffL);
      public static readonly Vector256<long> DECIMAL_MASK_FOR_DOUBLE = Vector256.Create(0xfffffffffffffL);
      public static readonly Vector256<long> EXPONENT_MASK_FOR_DOUBLE = Vector256.Create(1023L << 52);
      public static readonly Vector256<long> MAGIC_LONG_ADD = Vector256.AsInt64(Double.MAGIC_DOUBLE_ADD);

      public static readonly Vector256<long> MASK_SIGN = Vector256.Create(long.MaxValue);
      public static readonly Vector256<long> MASK_NOT_SIGN = Vector256.Create(~long.MaxValue);

      public static readonly Vector256<long> ONE = Vector256.Create(1l);
      public static readonly Vector256<long> NEGATIVE_ONE = Vector256.Create(-1l);
    }

    public static class Double {
      public static readonly Vector256<double> MASK_SIGN = Vector256.Create(long.MaxValue).AsDouble();
      public static readonly Vector256<double> MASK_NOT_SIGN = Vector256.Create(~long.MaxValue).AsDouble();
      public static readonly Vector256<double> MAGIC_DOUBLE_ADD = Vector256.Create(6755399441055744.0);

      public static readonly Vector256<double> ONE = Vector256.Create(1.0);
      public static readonly Vector256<double> NEGATIVE_ONE = Vector256.Create(-1.0);
    }
  }
}
