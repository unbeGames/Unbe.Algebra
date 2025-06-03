using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class ATan2 {
    public static readonly Float4[][] data = new[] {
      new [] { new Float4(0f), new Float4(1f) },
      new [] { new Float4(1f), new Float4(0) },
      new [] { new Float4(3f, 2f, 1f, 0.5f), new Float4(4f, 2f, 1f, 0.25f) },
      new [] { new Float4(-2), new Float4(3) },
      new [] { new Float4(-2), new Float4(-3) },
      new [] { new Float4(2), new Float4(-3) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void ATan2Theory(Float4 y, Float4 x) {
      var result = Maths.atan2(y, x);
      var expected = new Float4(MathF.Atan2(y.x, x.x), MathF.Atan2(y.y, x.y), MathF.Atan2(y.z, x.z), MathF.Atan2(y.w, x.w));

      Assert.That(AreApproxEqual(result, expected, 1e-5f), $"Expected {expected}, got {result}");
    }
  }
}
