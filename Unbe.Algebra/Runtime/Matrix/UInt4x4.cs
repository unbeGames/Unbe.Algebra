using System.Runtime.CompilerServices;

namespace Unbe.Algebra {
  [MathType(type: typeof(uint), dimensionX: 4, dimensionY: 4)]
  public partial struct UInt4x4 : IEquatable<UInt4x4>, IFormattable {
    /// <summary>Column 0 of the matrix.</summary>
    public UInt4 c0;
    /// <summary>Column 1 of the matrix.</summary>
    public UInt4 c1;
    /// <summary>Column 2 of the matrix.</summary>
    public UInt4 c2;
    /// <summary>Column 3 of the matrix.</summary>
    public UInt4 c3;
  }  
}
