using System.Numerics;
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

    /// <summary>Constructs a Float4x4 matrix from a Matrix4x4.</summary>
    /// <param name="m">Matrix4x4 to convert to Float4x4.</param>
    /// <returns>Constructed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float4x4(Matrix4x4 m) {
      c0 = new Float4(m.M11, m.M21, m.M31, m.M41);
      c1 = new Float4(m.M12, m.M22, m.M32, m.M42);
      c2 = new Float4(m.M13, m.M23, m.M33, m.M43);
      c3 = new Float4(m.M14, m.M24, m.M34, m.M44);
    }

    /// <summary>Constructs a Float4x4 vector from a Matrix4x4.</summary>
    /// <param name="v">Matrix4x4 to convert to Float4x4.</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float4x4(Matrix4x4 m) { return new Float4x4(m); }

    /// <summary>Constructs a Float4x4 vector from a Matrix4x4.</summary>
    /// <param name="m">Matrix4x4 to convert to Float4x4.</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Matrix4x4(Float4x4 m) {
      var c0 = m.c0;
      var c1 = m.c1;
      var c2 = m.c2;
      var c3 = m.c3;

      return new Matrix4x4(
        c0[0], c1[0], c2[0], c3[0],
        c0[1], c1[1], c2[1], c3[1],
        c0[2], c1[2], c2[2], c3[2],
        c0[3], c1[3], c2[3], c3[3]
      ); 
    }
  }  
}
