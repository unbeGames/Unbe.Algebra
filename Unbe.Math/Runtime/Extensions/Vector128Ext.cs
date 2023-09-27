using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Math {
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
  }
}
