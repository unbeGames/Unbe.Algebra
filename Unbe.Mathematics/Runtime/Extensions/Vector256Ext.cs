using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Math {
  public static class Vector256Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Reminder(Vector256<double> left, Vector256<double> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Reminder(Vector256<long> left, Vector256<long> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> Reminder(Vector256<ulong> left, Vector256<ulong> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(Vector256<double> vector, byte control) {
      if (Avx2.IsSupported) {
        return Avx2.Permute4x64(vector, control);
      }

      return Shuffle(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(Vector256<double> left, Vector256<double> right, byte control) {
      if (Avx.IsSupported) {
        return Avx.Shuffle(left, right, control);
      }

      return ShuffleSoftware(left, right, control);

      static Vector256<double> ShuffleSoftware(Vector256<double> left, Vector256<double> right, byte control) {
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

      static Vector256<double> SoftwareFallback(Vector256<double> vector) {
        return Vector256.Create(
            System.Math.Truncate(vector[0]),
            System.Math.Truncate(vector[1]),
            System.Math.Truncate(vector[2]),
            System.Math.Truncate(vector[3])
        );
      }
    }

    public static Vector256<T> FromLowHigh<T>(Vector128<T> low, Vector128<T> high) where T : struct {
      return low.ToVector256Unsafe().WithUpper(high);
    }
  }
}
