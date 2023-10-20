using Unbe.Algebra.CodeGen.Properties;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    private string underlyingType;

    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace($"{dimensionX}x{dimensionY}", string.Empty);
      underlyingType = $"{typeNameBase}{dimensionX}";

      //AddProps();
      AddConstructors();

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddConstructors() {
      string template = string.Empty;

      if(dimensionY == 4) {
        template = Resources.SimpleMatrix4Constructor;
      }

      sb.Append(string.Format(template, typeName, underlyingType, T, vectorPrefix));
    }

    private string Test() {
      return
$@"

";
    }
  }
}
