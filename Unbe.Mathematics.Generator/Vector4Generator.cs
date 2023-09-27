using System.Linq;
using System.Text;
using Unbe.Math.Generator.Properties;
using static Unbe.Math.Generator.Utils;

namespace Unbe.Math.Generator { 
  public class Vector4Generator { 
    private string typeName;
    private string T;
    private string vectorPrefix; 

    private readonly StringBuilder sb = new();
    private readonly StringBuilder tmp = new();

    public string Generate(string typeName, string targetType) {
      this.typeName = typeName;
      T = typeAliases[targetType];
      vectorPrefix = VectorPrefix(T, 4);

      sb.Clear();
      
      sb.Append(string.Format(Resources.Vector4Props, typeName, T));
      AddConstructors();
      AddAssingOperators();
      sb.Append(string.Format(Resources.BaseMathOperators, typeName, T, vectorPrefix));
      AddBitOperators();
      AddShuffles();
      sb.Append(string.Format(Resources.EqualsMethods, typeName));
      sb.Append(string.Format(Resources.Vector4StringMethods, typeName));
       
      return string.Format(Resources.BaseTemplate, typeName, sb.ToString());
    }

    private void AddConstructors() {
      sb.Append(string.Format(Resources.Vector4Constructors, typeName, T, vectorPrefix));
      
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
      tmp.Clear();

      var shuffle = Resources.Shuffle;
      var shuffleReadonly = Resources.ShuffleReadonly;

      for(int i = 0; i < shuffleNames.Length; i++) {
        var name = shuffleNames[i];
        var uniqueMembers = name.Distinct().Count() == 4;
        var template = uniqueMembers ? shuffle : shuffleReadonly;
        tmp.Append(string.Format(template, typeName, name, vectorPrefix));
      }
      sb.AppendLine();
      sb.Append(string.Format(Resources.ShuffleBase, tmp.ToString())); 
    }   

    private string Test() {
      return 
$@"

";
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
