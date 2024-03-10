namespace Unbe.Algebra.Tests.Operators {
  internal class SwizzleSetVector3 {
    public static readonly Float3[][] data = new[] {
      new Float3[] { new Float3(5), new Float3(0, 1, 2) },
    };

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZYX(Float3 result, Float3 target) {
      result.zyx = target;
      Assert.That(result, Is.EqualTo(new Float3(2, 1, 0)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZXW(Float3 result, Float3 target) {
      result.yzx = target;
      Assert.That(result, Is.EqualTo(new Float3(2, 0, 1)));
    }
    
    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYX(Float3 result, Float3 target) {
      result.yx = target.xy;
      Assert.That(result, Is.EqualTo(new Float3(1, 0, 5)), $"{new Float4(result.value)}");
    }


    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZX(Float3 result, Float3 target) {
      result.zx = target.xy;
      Assert.That(result, Is.EqualTo(new Float3(1, 5, 0)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZ(Float3 result, Float3 target) {
      result.yz = target.xy;
      Assert.That(result, Is.EqualTo(new Float3(5, 0, 1)));
    }
  }
}
