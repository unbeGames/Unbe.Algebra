using System.Runtime.CompilerServices;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 3, dimensionY: 3)]
  public partial struct Float3x3 : IEquatable<Float3x3>, IFormattable {
    /// <summary>Column 0 of the matrix.</summary>
    public Float3 c0;
    /// <summary>Column 1 of the matrix.</summary>
    public Float3 c1;
    /// <summary>Column 2 of the matrix.</summary>
    public Float3 c2;
  }

  public partial class Math {
    /// <summary>Returns an orthonormalized version of a Float3x3 matrix.</summary>
    /// <param name="m">The Float3x3 to be orthonormalized.</param>
    /// <returns>The orthonormalized Float3x3 matrix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float3x3 orthonormalize(Float3x3 m) {
      Float3x3 o;

      Float3 u = m.c0;
      Float3 v = m.c1 - m.c0 * dot(m.c1, m.c0);

      var lenU = length(u);
      var lenV = length(v);

      bool c = lenU > 1e-30f && lenV > 1e-30f;

      o.c0 = select(float3(1, 0, 0), u / lenU, c);
      o.c1 = select(float3(0, 1, 0), v / lenV, c);
      o.c2 = cross(o.c0, o.c1);

      return o;
    }
  }
}
