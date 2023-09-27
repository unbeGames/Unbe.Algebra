using System;
using System.Diagnostics;
using System.Text;
using Unbe.Mathematics.Generator.Properties;
using static Unbe.Mathematics2.Generator.Utils;

namespace Unbe.Mathematics2.Generator {
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
      sb.AppendLine("using System.Runtime.CompilerServices;");
      sb.AppendLine("using System.Runtime.Intrinsics;");
      sb.AppendLine();
      sb.AppendLine("namespace Unbe.Mathematics2 {");
      sb.Append($"  public partial struct {typeName} {{");
      sb.Append(string.Format(Resources.Vector4Props, typeName, T));
      AddConstructors();
      AddOperators();
      sb.Append(Equals());
      sb.Append(SimpleString());
      sb.Append(FormatString());
      sb.AppendLine("  }");
      sb.Append('}');
      return sb.ToString();
    }

    private void AddConstructors() {
      sb.Append(ConstructFrom4Components());
      sb.Append(ConstructFromVector4());
      sb.Append(ConstructFromVector128());
      sb.Append(ConstructFromSingleValue());
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

    private string ConstructFrom4Components() {
      return 
$@"
    /// <summary>Constructs a {typeName} vector from four {T} values.</summary>
    /// <param name=""x"">The constructed vector's x component will be set to this value.</param>
    /// <param name=""y"">The constructed vector's y component will be set to this value.</param>
    /// <param name=""z"">The constructed vector's z component will be set to this value.</param>
    /// <param name=""w"">The constructed vector's w component will be set to this value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}({T} x, {T} y, {T} z, {T} w) {{
      value = Vector128.Create(x, y, z, w);
    }}
";
    }

    private string ConstructFromVector4() {
      return
$@"
    /// <summary>Constructs a {typeName} vector from a {typeName} vector.</summary>
    /// <param name=""vector"">The constructed vector's components will be set to this value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}({typeName} vector) {{
      value = vector.value;
    }}
";
    }

    private string ConstructFromVector128() {
      return
$@"
    /// <summary>Constructs a {typeName} vector from Vector128<{T}>.</summary>
    /// <param name=""v"">Vector128<{T}> to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}(Vector128<{T}> v) {{
      value = v;
    }}
";
    }

    private string ConstructFromSingleValue() {
      return 
$@"
    /// <summary>Constructs a {typeName} vector from a single {T} value by assigning it to every component.</summary>
    /// <param name=""v"">{T} to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}({T} v) {{
      value = Vector128.Create(v);
    }}

    /// <summary>Constructs a {typeName} vector from a single bool value by converting it to {T} and assigning it to every component.</summary>
    /// <param name=""v"">bool to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}(bool v) {{
      value = Vector128.Create(v ? ({T})1 : ({T})0);
    }}

    /// <summary>Constructs a {typeName} vector from a single int value by converting it to {T} and assigning it to every component.</summary>
    /// <param name=""v"">int to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}(int v) {{
      value = Vector128.Create(v).As<int, {T}>();
    }}

    /// <summary>Constructs a {typeName} vector from a single double value by converting it to {T} and assigning it to every component.</summary>
    /// <param name=""v"">double to convert to float4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}(double v) {{
      value = Vector128.Create(({T})v);
    }}
";
    }

    private string Equals() {
      return 
$@"
    /// <summary>Returns true if the {typeName} is equal to a given {typeName}, false otherwise.</summary>
    /// <param name=""rhs"">Right hand side argument to compare equality with.</param>
    /// <returns>The result of the equality comparison.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals({typeName} other) {{
      return Vector128.EqualsAll(value, other.value);
    }}

    /// <summary>Returns true if the {typeName} is equal to a given {typeName}, false otherwise.</summary>
    /// <param name=""o"">Right hand side argument to compare equality with.</param>
    /// <returns>The result of the equality comparison.</returns>
    public override bool Equals(object o) {{ return o is {typeName} converted && Equals(converted); }}
";
    }

    private string FormatString() {
      return 
$@"
    public readonly string ToString(string format, IFormatProvider formatProvider) {{
      return string.Format(""{typeName}({{0}}f, {{1}}f, {{2}}f, {{3}}f)"", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider), w.ToString(format, formatProvider));
    }}
";
    }

    private string SimpleString() {
      return
$@"
    /// <summary>Returns a string representation of the {typeName}.</summary>
    /// <returns>String representation of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() {{
      return string.Format($""float4({{x}}f, {{y}}f, {{z}}f, {{w}}f)"");
    }}
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
