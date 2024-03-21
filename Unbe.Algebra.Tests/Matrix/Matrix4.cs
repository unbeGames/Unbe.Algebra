using System.Numerics;
using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Matrix4 {
    public static readonly Float4[][] quaternion = new[] {
      new [] { new Float4(0, 1, 0, Math.PI_HALF) },
      new [] { new Float4(1, 0, 0, -Math.PI / 4) },
      new [] { new Float4(0, 0, -1, 3 * Math.PI / 4) },
      new [] { new Float4(Math.normalize(new Float3(0, 1, -1)), -Math.PI / 3) }
    };

    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(quaternion))]
    public void Test(Float4 axisAngle) {
      var actual = Float4x4.AxisAngle(axisAngle.xyz, axisAngle.w);
      var expected = Matrix4x4.CreateFromAxisAngle(-axisAngle.xyz, axisAngle.w);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }
  }
}
