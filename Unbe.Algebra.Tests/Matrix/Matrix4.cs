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

    public static readonly Float4x4[][] matrix = new[] {
      new[] { new Float4x4(1, 2, 3, 4,  
                           0, 1, 2, 3,  
                           2, 3, 1, 4, 
                           2, 3, 4, 1) }
    };

    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(quaternion))]
    public void AxisAngleTheory(Float4 axisAngle) {
      var actual = Float4x4.AxisAngle(axisAngle.xyz, axisAngle.w);
      var expected = Matrix4x4.CreateFromAxisAngle(-axisAngle.xyz, axisAngle.w);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }


    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(matrix))]
    public void VectorMulMatrixTheory(Float4x4 matrix) {
      var expected = Vector4.Transform(matrix.c0, matrix);
      var actual = Math.mul(matrix.c0, matrix);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }

    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(matrix))]
    public void MatrixMulVectorTheory(Float4x4 matrix) {
      var expected = Vector4.Transform(matrix.c0, Matrix4x4.Transpose(matrix));
      var actual = Math.mul(matrix, matrix.c0);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }

    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(matrix))]
    public void MatrixMulMatrixTheory(Float4x4 matrix) {
      var expected = Matrix4x4.Multiply(matrix, matrix);
      var actual = Math.mul(matrix, matrix);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }

    [Theory]
    [Category("Matrix4x4")]
    [TestCaseSource(nameof(matrix))]
    public void TransposeTheory(Float4x4 matrix) {
      var expected = Matrix4x4.Transpose(matrix);
      var actual = Math.transpose(matrix);
      Assert.That(AreApproxEqual(actual, expected, 1e-6f), $"{actual} {expected}");
    }
  }
}
