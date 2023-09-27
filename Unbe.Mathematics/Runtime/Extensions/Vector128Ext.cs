using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Math {
  public static class Vector128Ext {
    public static Vector128<int> Reminder(Vector128<int> left, Vector128<int> right) {
      var n = left / right;

      return left - n * right;
    }

    public static Vector128<uint> Reminder(Vector128<uint> left, Vector128<uint> right) {
      var n = left / right;

      return left - n * right;
    }

    public static Vector128<float> Reminder(Vector128<float> left, Vector128<float> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    public static Vector128<float> Truncate(this Vector128<float> vector) {
      if (Sse41.IsSupported) {
        return Sse41.RoundToZero(vector);
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {
        return Vector128.Create(
            (float)System.Math.Truncate(vector[0]),
            (float)System.Math.Truncate(vector[1]),
            (float)System.Math.Truncate(vector[2]),
            (float)System.Math.Truncate(vector[3])
        );
      }
    }

    public static void GetLowHigh<T>(Vector256<T> vector, out Vector128<T> low, out Vector128<T> high) where T : struct {
      low = vector.GetLower();
      high = vector.GetUpper();
    }
  }
}
