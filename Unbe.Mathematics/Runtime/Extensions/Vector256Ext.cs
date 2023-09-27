using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Math {
  public static class Vector256Ext {
    public static Vector256<double> Reminder(Vector256<double> left, Vector256<double> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    public static Vector256<long> Reminder(Vector256<long> left, Vector256<long> right) {
      var n = left / right;

      return left - n * right;
    }

    public static Vector256<ulong> Reminder(Vector256<ulong> left, Vector256<ulong> right) {
      var n = left / right;

      return left - n * right;
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
