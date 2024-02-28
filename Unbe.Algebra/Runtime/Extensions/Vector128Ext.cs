using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector128Ext {
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Round(Vector128<float> vector) {
      if (Sse41.IsSupported) {
        return Sse41.RoundToNearestInteger(vector);
      }      

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {        
        return Vector128.Create(
            MathF.Round(vector[0]),
            MathF.Round(vector[1]),
            MathF.Round(vector[2]),
            MathF.Round(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetLowHigh<T>(in Vector256<T> vector, out Vector128<T> low, out Vector128<T> high) where T : struct {
      low = vector.GetLower();
      high = vector.GetUpper();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Sign(Vector128<float> vector) {
      var v = Vector128.Min(vector, Float.ONE);
      return Vector128.Max(v, Float.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> Sign(Vector128<int> vector) {
      var v = Vector128.Min(vector, Int.ONE);
      return Vector128.Max(v, Int.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float>ExtractSign(Vector128<float> vector) {
      return vector & Float.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> ExtractSign(Vector128<int> vector) {
      return vector & Int.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Pow(in Vector128<float> x, in Vector128<float> y) {
      return Exp(y * Log(x));
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

    internal static class Int {
      public static readonly Vector128<int> MASK_SIGN = Vector128.Create(int.MinValue);
      public static readonly Vector128<int> MASK_NOT_SIGN = Vector128.Create(~int.MaxValue);
      public static readonly Vector128<int> ONE = Vector128.Create(1);
      public static readonly Vector128<int> NEGATIVE_ONE = Vector128.Create(-1);

      public static readonly Vector128<int> ONE_HUNDRED_TWENTY_SEVEN = Vector128.Create(127);
      public static readonly Vector128<int> DECIMAL_MASK_FOR_FLOAT = Vector128.Create(8388607);
      public static readonly Vector128<int> EXPONENT_MASK_FOR_FLOAT = Vector128.Create(127 << 23);
    }

    internal static class Float {
      public static readonly Vector128<float> MASK_SIGN = Vector128.Create(int.MinValue).AsSingle();
      public static readonly Vector128<float> MASK_NOT_SIGN = Vector128.Create(~int.MaxValue).AsSingle();
      public static readonly Vector128<float> ONE = Vector128.Create(1f);
      public static readonly Vector128<float> NEGATIVE_ONE = Vector128.Create(-1f);
    }
  }
}
