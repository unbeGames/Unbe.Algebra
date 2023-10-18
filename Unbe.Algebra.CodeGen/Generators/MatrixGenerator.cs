using Unbe.Algebra.CodeGen.Properties;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace($"{dimensionX}x{dimensionY}", string.Empty);

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }
  }
}
