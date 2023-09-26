using System.Text;

namespace Unbe.Mathematics2.Generator {
  public class TemplateType4 {
    private string typeName;
    private string T;

    private readonly StringBuilder sb = new();    

    public string Generate(string typeName, string targetType) {
      this.typeName = typeName;
      this.T = Utils.typeAliases[targetType];
      sb.Clear();
      sb.AppendLine("using System.Runtime.CompilerServices;");
      sb.AppendLine("using System.Runtime.Intrinsics;");
      sb.AppendLine();
      sb.AppendLine("namespace Unbe.Mathematics2 {");
      sb.Append($"  public partial struct {typeName} {{");
      sb.Append(GetZero());
      sb.Append(PropertyGetters());
      sb.Append(ConstructorBase());
      sb.Append(ConstructorBase2());
      sb.Append(ConstructorVector());
      sb.Append(ConstructorSingle());
      sb.Append(ConstructFromSingleBool());
      sb.Append(Equals());
      sb.Append(SimpleString());
      sb.Append(FormatString());
      sb.AppendLine("  }");
      sb.Append('}');
      return sb.ToString();
    }

    private string ConstructorBase() {
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

    private string ConstructorSingle() {
      return 
$@"
    /// <summary>Constructs a {typeName} vector from a single {T} value by assigning it to every component.</summary>
    /// <param name=""v"">{T} to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}({T} v) {{
      value = Vector128.Create(v);
    }}
";
    }

    private string ConstructorBase2() {
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

    private string PropertyGetters() {
      return
$@"
    /// <summary>x component of the vector.</summary>
    public {T} x {{ readonly get {{ return value.GetElement(0); }} set {{ this.value = this.value.WithElement(0, value); }} }}
    /// <summary>y component of the vector.</summary>
    public {T} y {{ readonly get {{ return value.GetElement(1); }} set {{ this.value = this.value.WithElement(1, value); }} }}
    /// <summary>z component of the vector.</summary>
    public {T} z {{ readonly get {{ return value.GetElement(2); }} set {{ this.value = this.value.WithElement(2, value); }} }}
    /// <summary>w component of the vector.</summary>
    public {T} w {{ readonly get {{ return value.GetElement(3); }} set {{ this.value = this.value.WithElement(3, value); }} }}
";
    }

    private string ConstructorVector() {
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

    private string ConstructFromSingleBool() {
      return 
$@"
    /// <summary>Constructs a {typeName} vector from a single bool value by converting it to float and assigning it to every component.</summary>
    /// <param name=""v"">bool to convert to {typeName}</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public {typeName}(bool v) {{
      value = Vector128.Create(v).As<bool, {T}>();
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

    private string GetZero() {
      return
$@"
    /// <summary>{typeName} zero value.</summary>
    public static readonly {typeName} zero;
";
    }
  }
}
