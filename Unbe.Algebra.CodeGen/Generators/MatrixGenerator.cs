using Unbe.Algebra.CodeGen.Properties;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    private string underlyingType;

    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace($"{dimensionX}x{dimensionY}", string.Empty);
      underlyingType = $"{typeNameBase}{dimensionX}";

      AddProps();
      AddConstructors();

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddProps() {
      string propsTemplate = string.Empty;
      switch (dimensionY) {
        case 4:
          propsTemplate = Resources.Matrix4Props;
          break;
        case 3:
          propsTemplate = Resources.Matrix3Props;
          break;
      }
      //sb.Append(string.Format(Resources.VectorProperties, typeName, T));
      sb.Append(string.Format(propsTemplate, typeName));
      //sb.Append(string.Format(Resources.VectorIndexer, typeName, T, dimensionX));
    }

    private void AddConstructors() {
      string template = string.Empty;

      switch (dimensionY) {
        case 4:
          template = Resources.SimpleMatrix4Constructor;
          break;
        case 3:
          template = Resources.SimpleMatrix3Constructor;
          break;
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
