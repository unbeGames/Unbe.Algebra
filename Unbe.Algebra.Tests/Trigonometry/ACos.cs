using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class ACos {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Math.E, Math.SQRT2, 1 / Math.E, 1 / Math.SQRT2) },
      new Float4[] { new Float4(-20.5323f, 10.7f, 55, 99) },
      new Float4[] { new Float4(Math.PI, Math.PI_HALF, -Math.PI, -Math.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void ACosTheory(Float4 vector) {
      var result = Math.acos(vector);
      var expected = new Float4(MathF.Acos(vector.x), MathF.Acos(vector.y), MathF.Acos(vector.z), MathF.Acos(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-6f), $"Expected {expected}, got {result}");
    }
  }
}
