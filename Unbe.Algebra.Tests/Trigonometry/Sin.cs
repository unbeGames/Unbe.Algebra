using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Sin {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Maths.E, Maths.SQRT2, 1 / Maths.E, 1 / Maths.SQRT2) },
      new Float4[] { new Float4(-20.5323f, 10.7f, 55, 99) },
      new Float4[] { new Float4(Maths.PI, Maths.PI_HALF, -Maths.PI, -Maths.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void SinTheory(Float4 vector) {
      var result = Maths.sin(vector);
      var expected = new Float4(MathF.Sin(vector.x), MathF.Sin(vector.y), MathF.Sin(vector.z), MathF.Sin(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-6f), $"Expected {expected}, got {result}");
    }
  }
}
