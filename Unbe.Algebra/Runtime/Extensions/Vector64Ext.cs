using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  public static partial class Vector64Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> Reminder(in Vector64<int> left, in Vector64<int> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<uint> Reminder(in Vector64<uint> left, in Vector64<uint> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Reminder(in Vector64<float> left, Vector64<float> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    public static Vector64<float> Truncate(this Vector64<float> vector) {
      return Vector64.Create(
        MathF.Truncate(vector[0]),
        MathF.Truncate(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Round(Vector64<float> vector) { 
      return Vector64.Create(
        MathF.Round(vector[0]),
        MathF.Round(vector[1])
      );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Sign(Vector64<float> vector) {
      var v = Vector64.Min(vector, Float.ONE);
      return Vector64.Max(v, Float.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> Sign(Vector64<int> vector) {
      var v = Vector64.Min(vector, Int.ONE);
      return Vector64.Max(v, Int.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> ExtractSign(Vector64<float> vector) {
      return vector & Float.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<int> ExtractSign(Vector64<int> vector) {
      return vector & Int.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Pow(in Vector64<float> x, in Vector64<float> y) {
      return Exp(y * Log(x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> RSqrt(in Vector64<float> v) {
      return Float.ONE / Vector64.Sqrt(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Vector64<T> Select<T>(Vector64<T> falseVal, Vector64<T> trueVal, bool selector) where T : unmanaged {
      return selector ? trueVal : falseVal;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Vector64<T> Select<T>(Vector64<T> selector, Vector64<T> trueVal, Vector64<T> falseVal) where T : unmanaged {
      return (selector & trueVal) | (~selector & falseVal);
    }

    internal static class Int {
      public static readonly Vector64<int> MASK_SIGN = Vector64.Create(int.MinValue);
      public static readonly Vector64<int> MASK_NOT_SIGN = Vector64.Create(~int.MaxValue);
      public static readonly Vector64<int> ONE = Vector64.Create(1);
      public static readonly Vector64<int> NEGATIVE_ONE = Vector64.Create(-1);
    }

    internal static class Float {
      public static readonly Vector64<float> MASK_SIGN = Vector64.Create(int.MinValue).AsSingle();
      public static readonly Vector64<float> MASK_NOT_SIGN = Vector64.Create(~int.MaxValue).AsSingle();
      public static readonly Vector64<float> ONE = Vector64.Create(1f);
      public static readonly Vector64<float> NEGATIVE_ONE = Vector64.Create(-1f);
    }
  }
}
