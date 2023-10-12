using System.Text;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {
  internal abstract class BaseTypeGenerator {
    protected string typeName;
    protected string T;
    protected string vectorPrefix;
    protected string typeNameBase;
    protected int dimensionX, dimensionY;

    protected readonly StringBuilder sb = new();
    protected readonly StringBuilder sbMath = new();
    protected readonly StringBuilder tmp = new();

    public string Generate(string typeName, string targetType, int dimensionX, int dimensionY) {
      this.typeName = typeName;
      T = typeAliases[targetType];
      this.dimensionX = dimensionX;
      vectorPrefix = VectorPrefix(T, dimensionX, dimensionY);
      typeNameBase = typeName.Replace(dimensionX.ToString(), string.Empty);

      this.dimensionX = dimensionX;
      this.dimensionY = dimensionY;

      sb.Clear();
      sbMath.Clear();

      return GenerateInternal();
    }

    protected abstract string GenerateInternal();
  }
}
