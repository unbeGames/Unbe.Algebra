using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Math {
  public static class Vector128Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> Reminder(Vector128<int> left, Vector128<int> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> Reminder(Vector128<uint> left, Vector128<uint> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Shuffle(Vector128<float> vector, byte control) {
      if (Avx.IsSupported) {
        return Avx.Permute(vector, control);
      }

      return Shuffle(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Shuffle(Vector128<float> left, Vector128<float> right, byte control) {
      if (Sse.IsSupported) {
        return Sse.Shuffle(left, right, control);
      }

      return ShuffleSoftware(left, right, control);

      static Vector128<float> ShuffleSoftware(Vector128<float> left, Vector128<float> right, byte control) {
        const byte e0Mask = 0b_0000_0011, e1Mask = 0b_0000_1100, e2Mask = 0b_0011_0000, e3Mask = 0b_1100_0000;

        int e0Selector = control & e0Mask;
        float e0 = left.GetElement(e0Selector);

        int e1Selector = (control & e1Mask) >> 2;
        float e1 = left.GetElement(e1Selector);

        int e2Selector = (control & e2Mask) >> 4;
        float e2 = right.GetElement(e2Selector);

        int e3Selector = (control & e3Mask) >> 6;
        float e3 = right.GetElement(e3Selector);

        return Vector128.Create(e0, e1, e2, e3);
      }
    }

    public static void GetLowHigh<T>(Vector256<T> vector, out Vector128<T> low, out Vector128<T> high) where T : struct {
      low = vector.GetLower();
      high = vector.GetUpper();
    }
  }
}
