using Unbe.Algebra.CodeGen.Properties;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    private string underlyingType;

    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace($"{dimensionX}x{dimensionY}", string.Empty);
      underlyingType = $"{typeNameBase}{dimensionX}";


      if (dimensionY == 4) {
        AddProps(Resources.PropsMatrix4);
        AddConstructors(Resources.SimpleConstructorMatrix4);
        AddIndexer();
        AddBaseMath(Resources.BaseMathOperatorsMatrix4);
        AddSignMath(Resources.SignMathMatrix4);
        AddMatrixMath(Resources.MulMatrix4);
        AddEquality(Resources.EqualsMethodsMatrix4);
        if (dimensionX == 4) {
          AddExtConstructors(Resources.SimpleConstructorMatrix4x4);
          AddStringMethods(Resources.StringMethodsMatrix4x4);
        }
      } else if(dimensionY == 3) {
        AddProps(Resources.PropsMatrix3);
        AddConstructors(Resources.SimpleConstructorMatrix3);
        AddIndexer();
        AddBaseMath(Resources.BaseMathOperatorsMatrix3);
        AddSignMath(Resources.SignMathMatrix3);
        AddMatrixMath(Resources.MulMatrix3);
        AddEquality(Resources.EqualsMethodsMatrix3);
        if (dimensionX == 3) {
          AddExtConstructors(Resources.SimpleConstructorMatrix3x3);
          AddStringMethods(Resources.StringMethodsMatrix3x3);
        }
      }

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddProps(string propsTemplate) {      
      sb.Append(string.Format(propsTemplate, typeName));
    }

    private void AddConstructors(string template) {
      sb.Append(string.Format(template, typeName, underlyingType, T, vectorPrefix));
    }

    private void AddExtConstructors(string extTemplate) { 
      sb.Append(string.Format(extTemplate, typeName, underlyingType, T, vectorPrefix));
    }

    private void AddSignMath(string template) {
      if (IsSigned(numFlags)) {
        sbMath.Append(string.Format(template, typeName, vectorPrefix));
      }
    }

    private void AddMatrixMath(string template) {
      sbMath.Append(string.Format(template, typeName, underlyingType));
    }

    private void AddEquality(string template) {
      sb.Append(string.Format(template, typeName));
    }

    private void AddStringMethods(string template) {
      sb.Append(string.Format(template, typeName));
    }

    private void AddIndexer() {
      sb.Append(string.Format(Resources.MatrixIndexer, typeName, typeNameBase, dimensionX, dimensionY));
    }

    private void AddBaseMath(string template) {
      sb.Append(string.Format(template, typeName, T, vectorPrefix));
    }

    private string Test() {
      return
$@"
";
    }
  }
}
