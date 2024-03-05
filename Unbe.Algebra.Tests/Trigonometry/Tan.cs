using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Tan {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Math.E, Math.SQRT2, 1 / Math.E, 1 / Math.SQRT2) },
      new Float4[] { new Float4(-2.5323f, 1.7f, 5.5f, 9.9f) },
      new Float4[] { new Float4(Math.PI, Math.PI / 3, -Math.PI, -Math.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void TanTheory(Float4 vector) {
      var result = Math.tan(vector);
      var expected = new Float4(MathF.Tan(vector.x), MathF.Tan(vector.y), MathF.Tan(vector.z), MathF.Tan(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-5f), $"Expected {expected}, got {result}");
    }
  }
}
