using System.Runtime.CompilerServices;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 4, dimensionY: 4)]
  public partial struct Float4x4 /*: IEquatable<Float4x4>, IFormattable*/ {
    /// <summary>Column 0 of the matrix.</summary>
    public Float4 c0;
    /// <summary>Column 1 of the matrix.</summary>
    public Float4 c1;
    /// <summary>Column 2 of the matrix.</summary>
    public Float4 c2;
    /// <summary>Column 3 of the matrix.</summary>
    public Float4 c3;

    /// <summary>Constructs a float4x4 matrix from 16 float values given in row-major order.</summary>
    /// <param name="m00">The matrix at row 0, column 0 will be set to this value.</param>
    /// <param name="m01">The matrix at row 0, column 1 will be set to this value.</param>
    /// <param name="m02">The matrix at row 0, column 2 will be set to this value.</param>
    /// <param name="m03">The matrix at row 0, column 3 will be set to this value.</param>
    /// <param name="m10">The matrix at row 1, column 0 will be set to this value.</param>
    /// <param name="m11">The matrix at row 1, column 1 will be set to this value.</param>
    /// <param name="m12">The matrix at row 1, column 2 will be set to this value.</param>
    /// <param name="m13">The matrix at row 1, column 3 will be set to this value.</param>
    /// <param name="m20">The matrix at row 2, column 0 will be set to this value.</param>
    /// <param name="m21">The matrix at row 2, column 1 will be set to this value.</param>
    /// <param name="m22">The matrix at row 2, column 2 will be set to this value.</param>
    /// <param name="m23">The matrix at row 2, column 3 will be set to this value.</param>
    /// <param name="m30">The matrix at row 3, column 0 will be set to this value.</param>
    /// <param name="m31">The matrix at row 3, column 1 will be set to this value.</param>
    /// <param name="m32">The matrix at row 3, column 2 will be set to this value.</param>
    /// <param name="m33">The matrix at row 3, column 3 will be set to this value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float4x4(float m00, float m01, float m02, float m03,
                    float m10, float m11, float m12, float m13,
                    float m20, float m21, float m22, float m23,
                    float m30, float m31, float m32, float m33) {
      c0 = new Float4(m00, m10, m20, m30);
      c1 = new Float4(m01, m11, m21, m31);
      c2 = new Float4(m02, m12, m22, m32);
      c3 = new Float4(m03, m13, m23, m33);
    }
  }  
}
