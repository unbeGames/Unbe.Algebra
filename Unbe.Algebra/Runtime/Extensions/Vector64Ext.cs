using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  public static class Vector64Ext {
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
      return SoftwareFallback(vector);

      static Vector64<float> SoftwareFallback(in Vector64<float> vector) {
        return Vector64.Create(
          (float)System.Math.Truncate(vector[0]),
          (float)System.Math.Truncate(vector[1])
        );
      }
    }
  }
}
