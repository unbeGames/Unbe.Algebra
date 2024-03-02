using System.Numerics;
using System.Runtime.Intrinsics;

namespace Unbe.Algebra.Tests {
  internal static class TestHelpers {
    public static bool AreApproxEqual(Float4 left, Float4 right, float tolerance = 1e-7f) {
      return IsAlmostZero(Math.abs(left - right), tolerance);
    }

    public static bool IsAlmostZero(Float4 vector, float tolerance = 1e-7f) {
      bool result = true;
      for (int index = 0; index < 4; index++) {
        var num = vector.value[index];
        result = result && (float.IsNaN(num) || num < tolerance);
      }
      return result;
    }
  }
}
