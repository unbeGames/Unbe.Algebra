namespace Unbe.Algebra.Tests {
  public class Saturate {
    public static Float4[][] data = new[]{
      new Float4[] { new Float4(0.5f), new Float4(System.Math.Clamp(0.5f, 0f, 1f)) },
      new Float4[] { new Float4(-4f), new Float4(System.Math.Clamp(-4f, 0f, 1f)) },
      new Float4[] { new Float4(3f), new Float4(System.Math.Clamp(3f, 0f, 1f)) },
      new Float4[] { new Float4(float.PositiveInfinity), new Float4(System.Math.Clamp(float.PositiveInfinity, 0f, 1f)) },
      new Float4[] { new Float4(float.NegativeInfinity), new Float4(System.Math.Clamp(float.NegativeInfinity, 0f, 1f)) },
    };

    [Theory]
    [Category("Basic Maths")]
    [TestCaseSource(nameof(data))]
    public void SaturateTheory(Float4 vector, Float4 expected) {
      var result = Maths.saturate(vector);

      Assert.That(result, Is.EqualTo(expected), $"Expected {expected}, got {result}");
    }
  }
}
