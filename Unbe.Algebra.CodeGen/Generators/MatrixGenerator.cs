using Unbe.Algebra.CodeGen.Properties;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {
  internal class MatrixGenerator : BaseTypeGenerator {
    private string underlyingType;

    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace($"{dimensionX}x{dimensionY}", string.Empty);
      underlyingType = $"{typeNameBase}{dimensionX}";

      AddProps();
      AddConstructors();
      AddIndexer();
      AddBaseMath();
      AddMath();
      AddMatrixMath();
      AddEquality();
      AddStringMethods();

      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddProps() {
      string propsTemplate = string.Empty;
      switch (dimensionY) {
        case 4:
          propsTemplate = Resources.PropsMatrix4;
          break;
        case 3:
          propsTemplate = Resources.PropsMatrix3;
          break;
      }
      //sb.Append(string.Format(Resources.VectorProperties, typeName, T));
      sb.Append(string.Format(propsTemplate, typeName));
    }

    private void AddConstructors() {
      string template = string.Empty;
      string extTemplate = string.Empty;

      switch (dimensionY) {
        case 4:
          template = Resources.SimpleConstructorMatrix4;
          if(dimensionX == 4) {
            extTemplate = Resources.SimpleConstructorMatrix4x4;
          }
          break;
        case 3:
          template = Resources.SimpleConstructorMatrix3;
          if(dimensionX == 3) {
            extTemplate = Resources.SimpleConstructorMatrix3x3;
          }
          break;
      }

      sb.Append(string.Format(template, typeName, underlyingType, T, vectorPrefix));
      sb.Append(string.Format(extTemplate, typeName, underlyingType, T, vectorPrefix));
    }

    private void AddMath() {
      if (IsSigned(numFlags)) {
        string template = string.Empty;
        switch (dimensionY) {
          case 4:
            template = Resources.SignMathMatrix4;
            break;
          case 3:
            template = Resources.SignMathMatrix3;
            break;
        }
        sbMath.Append(string.Format(template, typeName, vectorPrefix));
      }
    }

    private void AddMatrixMath() {
      string template = string.Empty;
      switch (dimensionY) {
        case 4:
          template = Resources.MulMatrix4;
          break;
        case 3:
          template = Resources.MulMatrix3;
          break;
      }
      sbMath.Append(string.Format(template, typeName, underlyingType));
    }

    private void AddEquality() {
      string template = string.Empty;
      switch (dimensionY) {
        case 4:
          template = Resources.EqualsMethodsMatrix4;
          break;
        case 3:
          template = Resources.EqualsMethodsMatrix3;
          break;
      }
      sb.Append(string.Format(template, typeName));
    }

    private void AddStringMethods() {
      string template = string.Empty;
      if (dimensionX == 4) {
        switch (dimensionY) {
          case 4:
            template = Resources.StringMethodsMatrix4x4;
            break;
        }
      } else if(dimensionX == 3) {
        switch (dimensionY) {
          case 3:
            template = Resources.StringMethodsMatrix3x3;
            break;
        }
      }
      sb.Append(string.Format(template, typeName));
    }

    private void AddIndexer() {
      sb.Append(string.Format(Resources.MatrixIndexer, typeName, typeNameBase, dimensionX, dimensionY));
    }

    private void AddBaseMath() {
      string template = string.Empty;
      switch (dimensionY) {
        case 4:
          template = Resources.BaseMathOperatorsMatrix4;
          break;
        case 3:
          template = Resources.BaseMathOperatorsMatrix3;
          break;
      }
      sb.Append(string.Format(template, typeName, T, vectorPrefix));
    }

    private string Test() {
      return
$@"
";
    }
  }
}
