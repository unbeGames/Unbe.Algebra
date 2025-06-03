namespace Unbe.Algebra.Tests {
  internal static class TestHelpers {
    public static bool AreApproxEqual(Float4 left, Float4 right, float tolerance = 1e-7f) {
      return IsAlmostZero(Maths.abs(left - right), tolerance);
    }
        
    public static bool AreApproxEqual(Float4x4 left, Float4x4 right, float tolerance = 1e-7f) {
      return IsAlmostZero(Maths.abs(left - right), tolerance);
    }    

    public static bool IsAlmostZero(Float4 vector, float tolerance = 1e-7f) {
      bool result = true;
      for (int index = 0; index < vector.count; index++) {
        var num = vector[index];
        result = result && (float.IsNaN(num) || num < tolerance);
      }
      return result;
    }

    public static bool IsAlmostZero(Float4x4 m, float tolerance = 1e-7f) {
      bool result = true;
      for (int index = 0; index < 4; index++) {
        var num = m[index];
        result = result && IsAlmostZero(num, tolerance);
      }
      return result;
    }
  }
}
