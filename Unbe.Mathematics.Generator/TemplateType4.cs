using System;
using System.Text;
using Unbe.Math.Generator.Properties;
using static Unbe.Math.Generator.Utils;

namespace Unbe.Math.Generator { 
  public class TemplateType4 { 
    private string typeName;
    private string T;
    private Type TAsType; 

    private readonly StringBuilder sb = new();

    public string Generate(string typeName, string targetType) {
      this.typeName = typeName;
      T = typeAliases[targetType];
      TAsType = Type.GetType(T, false);
      sb.Clear();
      sb.Append(string.Format(Resources.Vector4Props, typeName, T));
      sb.Append(string.Format(Resources.Vector4Constructors, typeName, T, "Vector128"));
      AddOperators();
      sb.Append(string.Format(Resources.EqualsMethods, typeName));
      sb.Append(string.Format(Resources.Vector4StringMethods, typeName));
      return string.Format(Resources.BaseTemplate, typeName, sb.ToString());
    }

    private void AddOperators() {
      sb.Append(SingleToVectorOperator(typeName, T, T));
      if (TAsType != typeof(bool)) {
        sb.Append(SingleToVectorOperator(typeName, T, "bool"));
      }
      if (TAsType != typeof(int)) {
        sb.Append(SingleToVectorOperator(typeName, T, "int"));
      }
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
