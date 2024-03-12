using System.Text;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {

  internal abstract class BaseTypeGenerator {
    protected string typeName; // algebra type we are generating code for
    protected string T; // base type like float, int etc
    protected NumFlags numFlags;
    protected string vectorPrefix; // Vector64/128 that used in the algebra type
    protected string vectorType; // Vector64<T> / Vector128<T>
    protected string typeNameBase;
    protected int dimensionX, dimensionY;

    protected readonly StringBuilder sb = new();
    protected readonly StringBuilder sbMath = new();
    protected readonly StringBuilder tmp = new();

    public string Generate(string typeName, string targetType, int dimensionX, int dimensionY) {
      this.typeName = typeName;
      T = typeAliases[targetType];
      numFlags = typeToFlags[T];
      this.dimensionX = dimensionX;
      vectorPrefix = VectorPrefix(T, dimensionX, dimensionY);
      vectorType = $"{vectorPrefix}<{T}>";

      this.dimensionX = dimensionX;
      this.dimensionY = dimensionY;

      sb.Clear();
      sbMath.Clear();

      return GenerateInternal();
    }

    protected abstract string GenerateInternal();

  }
}
