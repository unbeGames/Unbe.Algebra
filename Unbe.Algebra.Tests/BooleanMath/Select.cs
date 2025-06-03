namespace Unbe.Algebra.Tests {
  internal class Select {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(-1f), new Float4(1f) },
      new Float4[] { new Float4(-1, 2, -3, 0.5f), new Float4(33, 15, MathF.PI, -MathF.Tau) },
    };

    [Theory]
    [Category("Boolean Maths")]
    [TestCaseSource(nameof(data))]
    public unsafe void SelectTrueTheory(Float4 vector1, Float4 vector2) {
      var result = Maths.select(vector1, vector2, true);
      Assert.That(result, Is.EqualTo(vector2));
    }

    [Theory]
    [Category("Boolean Maths")]
    [TestCaseSource(nameof(data))]
    public void SelectFalseTheory(Float4 vector1, Float4 vector2) {
      var result = Maths.select(vector1, vector2, false);
      Assert.That(result, Is.EqualTo(vector1));
    }
  }
}
