using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class ATan {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Maths.E, Maths.SQRT2, 1 / Maths.E, 1 / Maths.SQRT2) },
      new Float4[] { new Float4(-2.5323f, 1.7f, 5.5f, 9.9f) },
      new Float4[] { new Float4(Maths.PI, Maths.PI / 3, -Maths.PI, -Maths.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void ATanTheory(Float4 vector) {
      var result = Maths.atan(vector);
      var expected = new Float4(MathF.Atan(vector.x), MathF.Atan(vector.y), MathF.Atan(vector.z), MathF.Atan(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-5f), $"Expected {expected}, got {result}");
    }
  }
}
