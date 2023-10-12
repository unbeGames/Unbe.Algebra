using System.Runtime.Intrinsics;

namespace Unbe.Algebra {
  namespace Unbe.Algebra {
    [MathType(type: typeof(float), dimensionX: 4, dimensionY: 4)]
    public partial struct Float4x4 /*: IEquatable<Float4x4>, IFormattable*/ {
      public Vector128<float> c0;
      public Vector128<float> c1;
      public Vector128<float> c2;
      public Vector128<float> c3;
    }
  }
}
