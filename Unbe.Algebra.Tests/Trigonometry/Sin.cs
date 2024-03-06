using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static Unbe.Algebra.Tests.TestHelpers;

namespace Unbe.Algebra.Tests {
  internal class Sin {
    public static readonly Float4[][] data = new[] {
      new Float4[] { new Float4(0f) },
      new Float4[] { new Float4(-2f, -1f, 1f, 2f) },
      new Float4[] { new Float4(Math.E, Math.SQRT2, 1 / Math.E, 1 / Math.SQRT2) },
      new Float4[] { new Float4(-20.5323f, 10.7f, 55, 99) },
      new Float4[] { new Float4(Math.PI, Math.PI_HALF, -Math.PI, -Math.PI2) },
    };

    [Theory]
    [Category("Trigonometry")]
    [TestCaseSource(nameof(data))]
    public void SinTheory(Float4 vector) {
      var result = Math.sin(vector);
      var expected = new Float4(MathF.Sin(vector.x), MathF.Sin(vector.y), MathF.Sin(vector.z), MathF.Sin(vector.w));

      Assert.That(AreApproxEqual(result, expected, 1e-6f), $"Expected {expected}, got {result}");
    }
  }
}
