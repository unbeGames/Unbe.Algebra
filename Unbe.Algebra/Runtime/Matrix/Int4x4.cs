using System.Runtime.CompilerServices;

namespace Unbe.Algebra {
  [MathType(type: typeof(int), dimensionX: 4, dimensionY: 4)]
  public partial struct Int4x4 : IEquatable<Int4x4>, IFormattable {
    /// <summary>Column 0 of the matrix.</summary>
    public Int4 c0;
    /// <summary>Column 1 of the matrix.</summary>
    public Int4 c1;
    /// <summary>Column 2 of the matrix.</summary>
    public Int4 c2;
    /// <summary>Column 3 of the matrix.</summary>
    public Int4 c3;   
  }  
}
