namespace Unbe.Algebra.Tests.Operators {
  internal class SwizzleSetVector4 {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(5f), new Float4(0f, 1f, 2f, 3f) },
    };

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestWZYX(Float4 result, Float4 target) {
      result.wzyx = target;
      Assert.That(result, Is.EqualTo(new Float4(3f, 2f, 1f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZXW(Float4 result, Float4 target) {
      result.yzxw = target;
      Assert.That(result, Is.EqualTo(new Float4(2f, 0f, 1f, 3f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZYX(Float4 result, Float4 target) {
      result.zyx = target.xyz;
      Assert.That(result, Is.EqualTo(new Float4(2f, 1f, 0f, 5f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZXY(Float4 result, Float4 target) {
      result.zwy = target.xyz;
      Assert.That(result, Is.EqualTo(new Float4(5f, 2f, 0f, 1f)));
    }


    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYX(Float4 result, Float4 target) {
      result.yx = target.xy;
      Assert.That(result, Is.EqualTo(new Float4(1f, 0f, 5f, 5f)));
    }


    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestWZ(Float4 result, Float4 target) {
      result.wz = target.xy;
      Assert.That(result, Is.EqualTo(new Float4(5f, 5f, 1f, 0f)));
    }
  }
}
