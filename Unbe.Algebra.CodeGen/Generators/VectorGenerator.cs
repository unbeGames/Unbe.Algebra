using System.Linq;
using Unbe.Algebra.CodeGen.Properties;
using static Unbe.Algebra.CodeGen.Utils;

namespace Unbe.Algebra.CodeGen {
  internal class VectorGenerator : BaseTypeGenerator {     
    protected override string GenerateInternal() {
      typeNameBase = typeName.Replace(dimensionX.ToString(), string.Empty);

      AddProps();
      AddConstructors();
      AddAssingOperators();
      sb.Append(string.Format(Resources.BaseMathOperators, typeName, T, vectorPrefix));
      AddBitOperators();
      AddShuffles();
      sb.Append(string.Format(Resources.EqualsMethods, typeName));
      AddStringMethods();

      AddAdditionalMath();
       
      return string.Format(Resources.BaseTemplate, typeName, sb.ToString(), sbMath.ToString());
    }

    private void AddProps() {
      string propsTemplate = string.Empty;
      switch (dimensionX) {
        case 4:
          propsTemplate = Resources.Vector4Props;
          break;
        case 3:
          propsTemplate = Resources.Vector3Props;
          break;
        case 2:
          propsTemplate = Resources.Vector2Props;
          break;
      }
      sb.Append(string.Format(propsTemplate, typeName, T));
    }

    private void AddConstructors() {
      string vectorNConstructorTemplate = string.Empty;
      switch (dimensionX) {
        case 4:
          vectorNConstructorTemplate = Resources.Vector4Constructor;
          break;
        case 3:
          vectorNConstructorTemplate = Resources.Vector3Constructor;
          break;
        case 2:
          vectorNConstructorTemplate = Resources.Vector2Constructor;
          break;
      }

      sb.Append(string.Format(vectorNConstructorTemplate, typeName, T, vectorPrefix, typeNameBase));
      
      sb.Append(string.Format(Resources.SimpleVectorConstructor, typeName, T, vectorPrefix));

      sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, T));
      if (T != "int") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "int"));
      }
      if (T != "uint") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "uint"));
      }
      if (T != "float") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "float"));
      }
      if (T != "double") {
        sb.Append(SingleValueConstructor(typeName, T, vectorPrefix, "double"));
      }
    }

    private void AddAssingOperators() {
      sb.Append(SingleToVectorOperator(typeName, T, T));
      sb.Append(SingleToVectorOperator(typeName, T, $"{vectorPrefix}<{T}>"));
      if (T != "bool") {
        sb.Append(SingleToVectorOperator(typeName, T, "bool"));
      }
      if (T != "int") {
        sb.Append(SingleToVectorOperator(typeName, T, "int"));
      }
      if (T != "uint") {
        sb.Append(SingleToVectorOperator(typeName, T, "uint"));
      } 
      if (T != "float") {
        sb.Append(SingleToVectorOperator(typeName, T, "float"));
      }
      if (T != "double") {
        sb.Append(SingleToVectorOperator(typeName, T, "double"));
      } 
    }

    private void AddBitOperators() {
      if (SupportsBitOps(T)) {
        var rightOp = IsSigned(T) ? "ShiftRightArithmetic" : "ShiftRightLogical";
        sb.Append(string.Format(Resources.ShiftOperators, typeName, vectorPrefix, rightOp));
        sb.Append(string.Format(Resources.BitOperators, typeName, T, vectorPrefix));
      }
    }

    private void AddShuffles() {
      if (dimensionX == 2) return;
      tmp.Clear();

      var shuffle = Resources.Shuffle;
      var shuffleReadonly = Resources.ShuffleReadonly;
      var shuffleNames = shuffleByDimension[dimensionX];

      for(int i = 0; i < shuffleNames.Length; i++) {
        var name = shuffleNames[i];
        var uniqueMembers = name.Distinct().Count() == dimensionX;
        var template = uniqueMembers ? shuffle : shuffleReadonly; 
        tmp.Append(string.Format(template, typeName, name, vectorPrefix, dimensionX));
      }
      sb.AppendLine();
      sb.Append(string.Format(Resources.ShuffleBase, tmp.ToString())); 
    }

    private void AddAdditionalMath() {
      if(dimensionX == 4) {
        sbMath.Append(string.Format(Resources.Vector4Factory, typeName, T, typeNameBase, dimensionX));
      }
    }

    private string Test() {
      return 
$@"

";
    }

    private void AddStringMethods() {
      if (dimensionX == 4) {
        sb.Append(string.Format(Resources.Vector4StringMethods, typeName));
      } else if(dimensionX == 3) {
        sb.Append(string.Format(Resources.Vector3StringMethods, typeName));
      } else if(dimensionX == 2) {
        sb.Append(string.Format(Resources.Vector2StringMethods, typeName));
      }
    }

    private string SingleValueConstructor(string typeName, string T, string vectorPrefix, string targetType) {
      return string.Format(Resources.SingleValueConstructor, typeName, T, vectorPrefix, targetType);
    }

    private string SingleToVectorOperator(string typeName, string T, string targetType) {
      return string.Format(Resources.SingleToVectorOperator, ConvertToSingleArgs(typeName, T, targetType));
    }

    private string[] ConvertToSingleArgs(string typeName, string T, string targetType) {
      var operatorKind = ConvertOperator(targetType, T);
      var operatorSuffixed = AddSuffLyCap(operatorKind);
      var hint = T == targetType ? string.Empty : $"converting it to {T} and ";
      return new string[] { typeName, hint, targetType, operatorKind, operatorSuffixed };
    }
  }
}
