using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Sin {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(1f) },
      new Float4[] { new Float4(-Math.E) },
      new Float4[] { new Float4(MathF.Sqrt(2f)) },
      new Float4[] { new Float4(55) },
      new Float4[] { new Float4(Math.PI) },
      new Float4[] { new Float4(Math.PI_HALF) },      
      new Float4[] { new Float4(-Math.PI) },
      new Float4[] { new Float4(-Math.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void SinTheory(Float4 vector) {
      var result = Math.sin(vector);
      var expected = new Float4(MathF.Sin(vector.x), MathF.Sin(vector.y), MathF.Sin(vector.z), MathF.Sin(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-6f), $"Expected {expected}, got {result}");
    }
  }
}
