using System.Text;

namespace Unbe.Mathematics2.Generator {
  public class TemplateType4 {
    private string typeName;
    private string targetType;

    private readonly StringBuilder sb = new();    

    public string Generate(string typeName, string targetType) {
      this.typeName = typeName;
      this.targetType = Utils.typeAliases[targetType];
      sb.Clear();
      sb.Append("namespace Unbe.Mathematics2 {");
      sb.AppendLine();
      sb.AppendLine($"  public partial struct {typeName} {{");
      sb.AppendLine(GetZero());
      sb.AppendLine("  }");
      sb.Append('}');
      return sb.ToString();
    }

    private string GetZero() {
      return
$@"
    /// <summary>{typeName} zero value.</summary>
    public static readonly {typeName} zero;
";
    }
  }
}
