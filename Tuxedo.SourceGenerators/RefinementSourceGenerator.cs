using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Tuxedo.SourceGenerators;

[Generator]
public sealed class RefinementSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            (s, _) => IsRefinementMethod(s),
            (ctx, _) => (MethodDeclarationSyntax)ctx.Node
        );

        context.RegisterSourceOutput(
            context.CompilationProvider.Combine(provider.Collect()),
            (ctx, t) => GenerateCode(ctx, t.Left, t.Right)
        );
    }

    private static bool IsRefinementMethod(SyntaxNode s)
    {
        return s is MethodDeclarationSyntax mds
            // with the [Refinement] attribute
            && mds.AttributeLists.Any(als =>
                als.Attributes.Any(a => a.Name.ToString() == "Refinement")
            )
            // and is static
            && mds.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword))
            // and returns a bool
            && mds.ReturnType.ToString() == "bool"
            // and has at least one parameter and no more than two
            && mds.ParameterList.Parameters.Count is >= 1 and <= 2
            // and if there are two parameters, the second is an out parameter
            && (
                mds.ParameterList.Parameters.Count == 1
                || mds.ParameterList.Parameters[1]
                    .Modifiers.Any(m => m.IsKind(SyntaxKind.OutKeyword))
            );
    }

    private static void GenerateCode(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<MethodDeclarationSyntax> methodDeclarations
    )
    {
        foreach (var methodDeclaration in methodDeclarations)
        {
            var model = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
            var methodSymbol = model.GetDeclaredSymbol(methodDeclaration)!;
            var @namespace = methodSymbol.ContainingNamespace.ToDisplayString();
            var @class = methodSymbol.ContainingType.ToDisplayString();
            var name = methodDeclaration.Identifier.Text;
            var failureMessage = methodDeclaration
                .AttributeLists.SelectMany(als => als.Attributes)
                .First(a => a.Name.ToString() == "Refinement")
                .ArgumentList!.Arguments.First()
                .Expression.ToString();

            // get the first parameter type
            var parameterTypeSemanticModel = compilation.GetSemanticModel(
                methodDeclaration.ParameterList.Parameters.First().Type!.SyntaxTree
            );
            var parameterType = parameterTypeSemanticModel
                .GetTypeInfo(methodDeclaration.ParameterList.Parameters.First().Type!)
                .Type!.ToDisplayString();

            // get the generic type arguments
            var genericTypeArguments = methodSymbol.TypeArguments;
            var isGeneric = genericTypeArguments.Length > 0;
            var generics = isGeneric
                ? $"<{string.Join(", ", methodSymbol.TypeArguments.Select(t => t.ToDisplayString()))}>"
                : string.Empty;

            // get the constraints for the generic type arguments
            var genericTypeConstraints = isGeneric
                ? string.Join(
                    "\n",
                    methodDeclaration.ConstraintClauses.Select(x => x.ToString()).ToArray()
                )
                : string.Empty;

            var source = $$"""
                // <auto-generated/>
                #nullable enable

                using System.Diagnostics.CodeAnalysis;
                using Tuxedo;

                namespace {{@namespace}};

                /// <summary>
                /// Refinement implementation for {{@class}}.{{name}}
                /// </summary>
                public readonly partial struct {{name}}{{generics}} : IRefinement<{{name}}{{generics}}, {{parameterType}}>
                {{genericTypeConstraints}}
                {
                    /// <inheritdoc />
                    public bool CanBeRefined({{parameterType}} value, [NotNullWhen(false)] out string? failureMessage)
                    {
                        if ({{@class}}.{{name}}{{generics}}(value))
                        {
                            failureMessage = null;
                            return true;
                        }
                        failureMessage = ${{failureMessage}};
                        return false;
                    }
                }
                """;

            context.AddSource(
                $"{methodDeclaration.Identifier.Text}.{parameterType}.g.cs",
                SourceText.From(source, Encoding.UTF8)
            );
        }
    }
}
