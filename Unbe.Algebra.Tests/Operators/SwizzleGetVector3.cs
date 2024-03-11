using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra.Tests.Operators {
  internal class SwizzleGetVector3 {
    public static readonly Float3[][] data = new[] {
      new Float3[] { new Float3(0f, 1f, 2f) },
    };

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZYX(Float3 source) {
      Assert.That(source.zyx, Is.EqualTo(new Float3(2f, 1f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZX(Float3 source) {
      Assert.That(source.yzx, Is.EqualTo(new Float3(1f, 2f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestXX(Float3 source) {
      Assert.That(source.yy, Is.EqualTo(new Float2(1f, 1f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestZX(Float3 source) {
      Assert.That(source.zx, Is.EqualTo(new Float2(2f, 0f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYZ(Float3 source) {
      Assert.That(source.yz, Is.EqualTo(new Float2(1f, 2f)));
    }

    [Test]
    [Category("Swizzles")]
    [TestCaseSource(nameof(data))]
    public void TestYX(Float3 source) {
      Assert.That(source.yx, Is.EqualTo(new Float2(1f, 0f)));
    }
  }
}
