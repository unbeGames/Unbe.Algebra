using System.Linq;
using System.Text;
using Unbe.Math.Generator.Properties;
using static Unbe.Math.Generator.Utils;

namespace Unbe.Math.Generator { 
  public class Vector4Generator { 
    private string typeName;
    private string T;

    private readonly StringBuilder sb = new();
    private readonly StringBuilder tmp = new();

    public string Generate(string typeName, string targetType) {
      this.typeName = typeName;
      T = typeAliases[targetType];
      sb.Clear();
      
      sb.Append(string.Format(Resources.Vector4Props, typeName, T));
      sb.Append(string.Format(Resources.Vector4Constructors, typeName, T, "Vector128"));
      AddAssingOperators();
      sb.Append(string.Format(Resources.BaseMathOperators, typeName, T, "Vector128"));
      AddShuffles();
      sb.Append(string.Format(Resources.EqualsMethods, typeName));
      sb.Append(string.Format(Resources.Vector4StringMethods, typeName));
       
      return string.Format(Resources.BaseTemplate, typeName, sb.ToString());
    }

    private void AddAssingOperators() {
      sb.Append(SingleToVectorOperator(typeName, T, T));
      sb.Append(SingleToVectorOperator(typeName, T, $"Vector128<{T}>"));
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

    private void AddShuffles() {
      tmp.Clear();

      var shuffle = Resources.Shuffle;
      var shuffleReadonly = Resources.ShuffleReadonly;

      for(int i = 0; i < shuffleNames.Length; i++) {
        var name = shuffleNames[i];
        var uniqueMembers = name.Distinct().Count() == 4;
        var template = uniqueMembers ? shuffle : shuffleReadonly;
        tmp.Append(string.Format(template, typeName, name, "Vector128"));
      }
      sb.AppendLine();
      sb.Append(string.Format(Resources.ShuffleBase, tmp.ToString())); 
    }   

    private string Test() {
      return 
$@"

";
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
