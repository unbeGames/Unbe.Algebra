namespace Unbe.Algebra.Tests {
  internal class FloatVector {
    public static readonly Float4[][] lessGreater = new[] {
      new [] { new (5), new (14), (Float4)new Bool4(true) },
      new [] { new (-200), new (-200), (Float4)new Bool4(false) },
      new [] { new (1.5f), new (0), (Float4)new Bool4(false) },
      new [] { new (0, 0, 10, 10), new (1, 1, 5f, 5f), (Float4)new Bool4(true, true, false, false) },
      new [] { new (0, 0, 10, 10), new (-5, -5, 21f, 11f), (Float4)new Bool4(false, false, true, true) },
    };

    [Theory]
    [Category("Float")]
    [TestCaseSource(nameof(lessGreater))]
    public unsafe void LessTheory(Float4 vector1, Float4 vector2, Float4 expected) {
      var result = vector1 < vector2;
      Assert.That((Float4)result, Is.EqualTo(expected));
    }

    [Theory]
    [Category("Float")]
    [TestCaseSource(nameof(lessGreater))]
    public unsafe void GreaterTheory(Float4 vector1, Float4 vector2, Float4 expected) {
      var result = vector1 > vector2;
      if (!vector1.Equals(vector2)) {
        result = !result;
      }
      Assert.That((Float4)result, Is.EqualTo(expected));
    }
  }
}
