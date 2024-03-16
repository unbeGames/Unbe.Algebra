namespace Unbe.Algebra.Tests {
  internal class QuaternionTest {
    public static readonly Float4[][] data = new[] {
      new [] { new Float4(0, 1, 0, Math.PI_HALF) },
      new [] { new Float4(1, 0, 0, -Math.PI / 4) },
      new [] { new Float4(0, 0, -1, 3 * Math.PI / 4) },
      new [] { new Float4(Math.normalize(new Float3(0, 1, -1)), -Math.PI / 3) }
    };

    [Theory]
    [Category("Quaternion")]
    [TestCaseSource(nameof(data))]
    public void Test(Float4 axisAngle) {
      var actual = Quaternion.AxisAngle(axisAngle.xyz, axisAngle.w);
      var expected = System.Numerics.Quaternion.CreateFromAxisAngle(axisAngle.xyz, axisAngle.w);
      Assert.That(actual, Is.EqualTo((Quaternion)expected));
    }
  }
}
