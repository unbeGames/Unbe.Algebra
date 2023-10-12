using Unbe.Algebra.CodeGen.Properties;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    protected override string GenerateInternal() {
      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }
  }
}
