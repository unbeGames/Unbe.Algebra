using System.Numerics;

namespace Unbe.Algebra.Tests {
  internal static class TestHelpers {
    public static Vector4 ToVector4(Float4 vector) {
      return new Vector4(vector.x, vector.y, vector.z, vector.w);
    }
  }
}
