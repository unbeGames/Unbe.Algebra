namespace Unbe.Algebra.Tests {
  internal class Abs {
    public static Float4[][] data = new[] {
      new Float4[] { new Float4(0f), new Float4(0f) },
      new Float4[] { new Float4(1f), new Float4(1f) },
      new Float4[] { new Float4(-1f), new Float4(1f) },
      new Float4[] { new Float4(-1f, 0f, -1f, 0f), new Float4(1f, 0f, 1f, 0f) },
      new Float4[] { new Float4(float.NegativeInfinity), new Float4(float.PositiveInfinity) },
      new Float4[] { new Float4(float.NaN, -111f, -0.0000001f, 0.0000001f), new Float4(float.NaN, 111f, 0.0000001f, 0.0000001f) },
    };

    [Theory]
    [Category("Math")]
    [TestCaseSource(nameof(data))]
    public void AbsTheory(Float4 vector, Float4 expected) {
      var result = Math.abs(vector);

      Assert.That(result, Is.EqualTo(expected), $"Expected {expected}, got {result}");
    }
  }
}
