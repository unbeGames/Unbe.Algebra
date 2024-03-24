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
          propsTemplate = Resources.Matrix4Props;
          break;
        case 3:
          propsTemplate = Resources.Matrix3Props;
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
          template = Resources.SimpleMatrix4Constructor;
          if(dimensionX == 4) {
            extTemplate = Resources.SimpleMatrix4x4Constructor;
          }
          break;
        case 3:
          template = Resources.SimpleMatrix3Constructor;
          if(dimensionX == 3) {
            extTemplate = Resources.SimpleMatrix3x3Constructor;
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
          template = Resources.MatrixMul4;
          break;
        case 3:
          template = Resources.MatrixMul3;
          break;
      }
      sbMath.Append(string.Format(template, typeName, underlyingType));
    }

    private void AddEquality() {
      string template = string.Empty;
      switch (dimensionY) {
        case 4:
          template = Resources.Matrix4EqualsMethods;
          break;
        case 3:
          template = Resources.Matrix3EqualsMethods;
          break;
      }
      sb.Append(string.Format(template, typeName));
    }

    private void AddStringMethods() {
      string template = string.Empty;
      if (dimensionX == 4) {
        switch (dimensionY) {
          case 4:
            template = Resources.Matrix4x4StringMethods;
            break;
        }
      } else if(dimensionX == 3) {
        switch (dimensionY) {
          case 3:
            template = Resources.Matrix3x3StringMethods;
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
          template = Resources.BaseMathMatrix4Operators;
          break;
        case 3:
          template = Resources.BaseMathMatrix3Operators;
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
