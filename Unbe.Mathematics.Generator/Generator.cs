using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Unbe.Math.Generator {
  [Generator]
  public class TypeGenerator : IIncrementalGenerator {
    private const string AttributeShortName = "MathType";
    private const string AttributeFullName = $"{AttributeShortName}Attribute";

    private readonly TemplateType4 templateType4 = new();

    public void Initialize(IncrementalGeneratorInitializationContext context) {
      //Debugger.Launch();
      var comparer = TypeComparer.Instance;

      var provider = context.SyntaxProvider.CreateSyntaxProvider(
        predicate: (node, _) => NodePredicate(node),
        transform: (n, c) => TargetFactory(n, c)
      ).Where(m => m is not null)
      .WithComparer(comparer) // B1
      .Collect() // B2
      .SelectMany((targets, _) => targets.Distinct(comparer)) // B3
      .Collect(); // C;

      context.RegisterSourceOutput(provider, Build);

      Console.WriteLine("Execute code generator");
    }

    private void Build(SourceProductionContext context, ImmutableArray<BuilderTarget> source) {
      Debug.WriteLine("Build code generator");     
      
      foreach (var target in source) {      
        var sourceCode = templateType4.Generate(target.Type.Name, target.TargetType);

        context.AddSource($"{target.Type.Name}.g.cs", sourceCode);
      }
      //File.WriteAllText("C:\\Unity\\Unbe.Mathematics.Generator\\log.txt", builder.ToString());
    }

    private static BuilderTarget TargetFactory(GeneratorSyntaxContext context, CancellationToken cancellationToken) {
      var attr = (AttributeSyntax)context.Node;
      var model = context.SemanticModel;

      var declaration = (StructDeclarationSyntax)context.Node.Parent.Parent; // F1

      var arguments = attr.ArgumentList.Arguments.ToArray() ?? Array.Empty<AttributeArgumentSyntax>();
      var targetType = model.GetTypeInfo((GetArgument(arguments, "type") as TypeOfExpressionSyntax).Type).Type.MetadataName;
      var dimensions = (int)(GetArgument(arguments, "dimensions") as LiteralExpressionSyntax).Token.Value;

      return model.GetDeclaredSymbol(declaration, cancellationToken) is INamedTypeSymbol type
          ? new(declaration, type, targetType, dimensions)
          : null;
    }

    private static ExpressionSyntax GetArgument(AttributeArgumentSyntax[] arguments, string argName) {
      return Array.FindIndex(arguments, a => a.NameColon.Name.ToString() == argName) is int index and not -1
            ? arguments[index].Expression : null;
    }

    private static bool NodePredicate(SyntaxNode node)
        => node is AttributeSyntax attribute // E1
            && GetNameText(attribute.Name) is AttributeShortName or AttributeFullName // E2
            && !ContainsErrors(attribute) // E3
            && attribute.Parent.Parent is StructDeclarationSyntax; // E4

    private static string GetNameText(NameSyntax name)
        => name switch {
          SimpleNameSyntax ins => ins.Identifier.Text,
          QualifiedNameSyntax qns => qns.Right.Identifier.Text,
          _ => null,
        };

    private static bool ContainsErrors(CSharpSyntaxNode node)
       => node
           .GetDiagnostics()
           .Any(d => d.Severity == DiagnosticSeverity.Error);

    private sealed record BuilderTarget(StructDeclarationSyntax Declaration, INamedTypeSymbol Type, string TargetType, int dimensions);

    private sealed class TypeComparer : IEqualityComparer<BuilderTarget> {
      private TypeComparer() {
      }

      public static TypeComparer Instance { get; } = new();

      public bool Equals(BuilderTarget x, BuilderTarget y)
          => x.Type.Equals(y.Type, SymbolEqualityComparer.IncludeNullability);

      public int GetHashCode(BuilderTarget obj)
          => SymbolEqualityComparer.IncludeNullability.GetHashCode(obj.Type);
    }
  }
}
