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
  }  
}
