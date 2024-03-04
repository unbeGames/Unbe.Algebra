using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class SinCos {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Math.E, Math.SQRT2, 1 / Math.E, 1 / Math.SQRT2) },
      new Float4[] { new Float4(-3.5323f, 4.7f, 5.5f, 9.9f) },
      new Float4[] { new Float4(Math.PI, Math.PI_HALF, Math.PI / 3, -Math.PI2) }
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void SinCosTheory(Float4 vector) {
      Math.sincos(vector, out var resultSin, out var resultCos);
      
      var sinCosX = MathF.SinCos(vector.x);
      var sinCosY = MathF.SinCos(vector.y);
      var sinCosZ = MathF.SinCos(vector.z);
      var sinCosW = MathF.SinCos(vector.w);
      var expectedSin = new Float4(sinCosX.Sin, sinCosY.Sin, sinCosZ.Sin, sinCosW.Sin);
      var expectedCos = new Float4(sinCosX.Cos, sinCosY.Cos, sinCosZ.Cos, sinCosW.Cos);
      
      Assert.Multiple(() => {
        Assert.That(AreApproxEqual(resultSin, expectedSin, 1e-6f), $"Expected {resultSin}, got {expectedSin}");
        Assert.That(AreApproxEqual(resultCos, expectedCos, 1e-6f), $"Expected {resultCos}, got {expectedCos}");
      });
    }
  }
}
