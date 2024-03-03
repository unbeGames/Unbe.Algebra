namespace Unbe.Algebra.Tests {
  public class Add {
    public static readonly Float4[][] data = new[]{
      new Float4[] { new Float4(0f), new Float4(0f), new Float4(0f + 0f) },
      new Float4[] { new Float4(1f), new Float4(1f), new Float4(1f + 1f) },
      new Float4[] { new Float4(-1f), new Float4(1f), new Float4(-1f + 1f) },
      new Float4[] { new Float4(-1f, 0f, -1f, 0f), new Float4(1f, 10f, 1f, 10f), new Float4(-1f + 1f, 0f + 10f, -1f + 1f, 0f + 10f) },
      new Float4[] { new Float4(float.NegativeInfinity), new Float4(float.PositiveInfinity), new Float4(float.NegativeInfinity + float.PositiveInfinity) },
      new Float4[] { new Float4(float.MinValue), new Float4(float.MaxValue), new Float4(float.MinValue + float.MaxValue) },
    };

    [Theory]
    [Category("Basic Math")]
    [TestCaseSource(nameof(data))]
    public void AddTheory(Float4 left, Float4 right, Float4 expected) {
      var result = left + right;

      Assert.That(result, Is.EqualTo(expected), $"Expected {expected}, got {result}");
    }
  }
}
