using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Cos {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Math.E, Math.SQRT2, 1 / Math.E, 1 / Math.SQRT2) },
      new Float4[] { new Float4(-3.5323f, 4.7f, 5.5f, 9.9f) },
      new Float4[] { new Float4(Math.PI, Math.PI_HALF, -Math.PI, -Math.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void CosTheory(Float4 vector) {
      var result = Math.cos(vector);
      var expected = new Float4(MathF.Cos(vector.x), MathF.Cos(vector.y), MathF.Cos(vector.z), MathF.Cos(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-6f), $"Expected {expected}, got {result}");
    }
  }
}
