namespace Unbe.Algebra.Tests {
  public class Clamp {
    public static readonly Float4[][] data = new[]{
      new Float4[] { new Float4(0f), new Float4(0f), new Float4(0f), new Float4(System.Math.Clamp(0f, 0f, 0f)) },
      new Float4[] { new Float4(1f), new Float4(0f), new Float4(1f), new Float4(System.Math.Clamp(1f, 0f, 1f)) },
      new Float4[] { new Float4(-3f), new Float4(-4f), new Float4(-2f), new Float4(System.Math.Clamp(-3f, -4f, -2f)) },
      new Float4[] { new Float4(1f), new Float4(10f), new Float4(110f), new Float4(System.Math.Clamp(1f, 10f, 110f)) },
      new Float4[] { new Float4(float.NegativeInfinity), new Float4(0f), new Float4(1f), new Float4(System.Math.Clamp(float.NegativeInfinity, 0f, 1f)) },
      new Float4[] { new Float4(float.PositiveInfinity), new Float4(0f), new Float4(100f), new Float4(System.Math.Clamp(float.PositiveInfinity, 0f, 100f)) },
    };

    [Theory]
    [Category("Basic Math")]
    [TestCaseSource(nameof(data))]
    public void ClampTheory(Float4 vector, Float4 low, Float4 high, Float4 expected) {
      var result = Math.clamp(vector, low, high);

      Assert.That(result, Is.EqualTo(expected), $"Expected {expected}, got {result}");
    }
  }
}
