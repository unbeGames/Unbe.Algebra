using System.Runtime.CompilerServices;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 3, dimensionY: 3)]
  public partial struct Float3x3 /*: IEquatable<Float3x3>, IFormattable*/ {
    /// <summary>Column 0 of the matrix.</summary>
    public Float3 c0;
    /// <summary>Column 1 of the matrix.</summary>
    public Float3 c1;
    /// <summary>Column 2 of the matrix.</summary>
    public Float3 c2;
  }  
}
