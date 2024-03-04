using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  public static partial class Vector64Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Sin(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Sin(vector[0]),
        MathF.Sin(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Cos(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Cos(vector[0]),
        MathF.Cos(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SinCos(in Vector64<float> vector, out Vector64<float> sin, out Vector64<float> cos) {
      sin = Sin(vector);
      cos = Cos(vector);
    }
  } 
}
