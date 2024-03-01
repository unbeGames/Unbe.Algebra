using System.Numerics;

namespace Unbe.Algebra.Tests {
  internal class DotProduct {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f), new Float4(0f) },
      new Float4[] { new Float4(1f), new Float4(1f) },
      new Float4[] { new Float4(-1f), new Float4(-1f) },
      new Float4[] { new Float4(-1f), new Float4(1f, 4f, 9f, 16f) },
      new Float4[] { new Float4(1f, 2f, 3f, 4f), new Float4(4f, -5f, 6f, 9f) },
      new Float4[] { new Float4(float.PositiveInfinity), new Float4(float.PositiveInfinity) },
      new Float4[] { new Float4(float.PositiveInfinity), new Float4(float.NegativeInfinity) },
      new Float4[] { new Float4(float.NaN), new Float4(float.NegativeInfinity) },
      new Float4[] { new Float4(float.MaxValue, float.MinValue, float.NaN, 0), new Float4(float.MaxValue, float.MinValue, float.NaN, 0) },
    };
       
    [Theory]
    [Category("VectorOperations")]
    [TestCaseSource(nameof(data))]
    public void DotProductTheory(Float4 left, Float4 right) {
      var result = Math.dot(left, right);
      Float4 expected = Vector4.Dot(left, right);

      Assert.That(result, Is.EqualTo(expected), $"Expected {expected}, got {result}");
    }
  }
}
