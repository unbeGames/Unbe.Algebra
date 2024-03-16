namespace Unbe.Algebra.Tests {
  internal class BoolVector {
    public static readonly Bool4[][] data = new[] {
      new Bool4[] { new Bool4(false, true, true, false), new Bool4(false, true, true, false), new Bool4(true) },
      new Bool4[] { new Bool4(false, true, true, false), new Bool4(true, false, false, true), new Bool4(false) },
      new Bool4[] { new Bool4(false, true, true, false), new Bool4(true, false, true, false), new Bool4(false, false, true, true) },
      new Bool4[] { new Bool4(false, true, true, false), new Bool4(false, true, false, true), new Bool4(true, true, false, false) },
    };

    [Theory]
    [Category("Bool")]
    [TestCaseSource(nameof(data))]
    public unsafe void EqualityTheory(Bool4 vector1, Bool4 vector2, Bool4 expected) {
      var result = vector1 == vector2;
      Assert.That(result, Is.EqualTo(expected));
    }

    [Theory]
    [Category("Bool")]
    [TestCaseSource(nameof(data))]
    public unsafe void InEqualityTheory(Bool4 vector1, Bool4 vector2, Bool4 expected) {
      var result = vector1 != vector2;
      Assert.That(result, Is.EqualTo(!expected));
    }
  }
}
