using System.Numerics;
using System.Runtime.Intrinsics;
using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Normalize {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(1f) },
      new Float4[] { new Float4(-1f) },
      new Float4[] { new Float4(-1f) },
      new Float4[] { new Float4(1f, 2f, 3f, 4f) },
      new Float4[] { new Float4(15324.32354253f, 0.00000000213112f, 3222222222222222222222f, 4.2342342222222222f) },
      new Float4[] { new Float4(float.PositiveInfinity) },
      new Float4[] { new Float4(float.PositiveInfinity) },
      new Float4[] { new Float4(float.NaN) },
      new Float4[] { new Float4(float.MaxValue, float.MinValue, float.NaN, 0) },
    };
       
    [Theory]
    [Category("VectorOperations")]
    [TestCaseSource(nameof(data))]
    public void NormalizeTheory(Float4 vector) {
      Float4 result = Math.normalize(vector);
      Float4 expected = Vector4.Normalize(vector);

      Assert.That(AreApproxEqual(result, expected, 1e-7f), $"Expected {expected}, got {result}");
    }
  }
}
