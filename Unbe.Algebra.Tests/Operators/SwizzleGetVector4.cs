namespace Unbe.Algebra.Tests.Operators {
  internal class SwizzleGetVector4 {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f, 1f, 2f, 3f) },
    };

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestWZYX(Float4 source) {
      Assert.That(source.wzyx, Is.EqualTo(new Float4(3f, 2f, 1f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZXW(Float4 source) {
      Assert.That(source.yzxw, Is.EqualTo(new Float4(1f, 2f, 0f, 3f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZYX(Float4 source) {
      Assert.That(source.zyx, Is.EqualTo(new Float3(2f, 1f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZXY(Float4 source) {
      Assert.That(source.zwy, Is.EqualTo(new Float3(2f, 3f, 1f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestXX(Float4 source) {
      Assert.That(source.yy, Is.EqualTo(new Float2(1f, 1f)));
    }


    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestWZ(Float4 source) {
      Assert.That(source.wz, Is.EqualTo(new Float2(3f, 2f)));
    }
  }
}
