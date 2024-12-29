using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Tuxedo.SourceGenerator;

/// <summary>
/// Primary source generator that builds refined types from predicate methods
/// </summary>
[Generator]
public sealed partial class RefinementSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(
            context.CompilationProvider,
            (ctx, _) => ctx.AddSource("RefinementAttribute.g", RefinementAttributeSource)
        );

        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            (s, _) => IsRefinementMethod(s),
            (ctx, _) => (MethodDeclarationSyntax)ctx.Node
        );
        context.RegisterSourceOutput(
            context.CompilationProvider.Combine(provider.Collect()),
            (ctx, t) => GenerateRefinedTypes(ctx, t.Left, t.Right)
        );
    }

    private static bool IsRefinementMethod(SyntaxNode s)
    {
        return s is MethodDeclarationSyntax mds
            // with the [Refinement] attribute
            && mds.AttributeLists.Any(als =>
                als.Attributes.Any(a =>
                    string.Equals(a.Name.ToString(), "Refinement", StringComparison.Ordinal)
                )
            )
            // and is static
            && mds.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword))
            // and returns a bool
            && string.Equals(mds.ReturnType.ToString(), "bool", StringComparison.Ordinal)
            // and has at least one parameter and no more than two
            && mds.ParameterList.Parameters.Count is >= 1 and <= 2
            // and if there are two parameters, the second is an out parameter
            && (
                mds.ParameterList.Parameters.Count == 1
                || mds.ParameterList.Parameters[1]
                    .Modifiers.Any(m => m.IsKind(SyntaxKind.OutKeyword))
            );
    }

    private static void GenerateRefinedTypes(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<MethodDeclarationSyntax> methodDeclarations
    )
    {
        foreach (var methodDeclaration in methodDeclarations)
        {
            var viewModel = new MultiRefinedTypeModel();

            var model = compilation.GetSemanticModel(methodDeclaration.SyntaxTree);
            var methodSymbol = model.GetDeclaredSymbol(methodDeclaration)!;
            viewModel.Namespace = methodSymbol.ContainingNamespace.ToDisplayString();

            var containingType = methodSymbol.ContainingType;
            var @class = containingType.ToDisplayString();
            var name = methodDeclaration.Identifier.Text;
            viewModel.Predicate = $"{@class}.{name}";

            var attributeParts = new RefinementAttributeParts(methodDeclaration);
            viewModel.FailureMessage = attributeParts.FailureMessage;
            viewModel.AccessModifier = attributeParts.AccessModifier;

            ExtractGenericPartDetails(
                methodSymbol,
                methodDeclaration,
                out var generics,
                out var genericTypeConstraints
            );
            viewModel.Generics = generics;
            viewModel.GenericConstraints = genericTypeConstraints;

            // get the first parameter type
            var firstParam = methodDeclaration.ParameterList.Parameters[0].Type!;
            var firstParamSemanticModel = compilation.GetSemanticModel(firstParam.SyntaxTree);
            var firstParameterTypeInfo = firstParamSemanticModel.GetTypeInfo(firstParam).Type;
            viewModel.RawType = firstParameterTypeInfo!.ToDisplayString();

            viewModel.RefinedType = BuildSafeStructName(
                attributeParts.Name ?? name,
                attributeParts.Name is not null ? null : viewModel.SafeRawTypeName()
            );

            // try and see if there is a second out parameter
            string source;
            if (methodDeclaration.ParameterList.Parameters.Count > 1)
            {
                var secondParam = methodDeclaration.ParameterList.Parameters[1].Type!;
                var secondParamSemanticModel = compilation.GetSemanticModel(secondParam.SyntaxTree);
                var secondParameterTypeInfo = secondParamSemanticModel
                    .GetTypeInfo(secondParam)
                    .Type;
                viewModel.AlternativeType = secondParameterTypeInfo!.ToDisplayString();
                source = RenderMultiRefinedType(viewModel);
            }
            else
            {
                source = RenderSingleRefinedType(viewModel);
            }

            context.AddSource(
                $"{viewModel.RefinedType}.g.cs",
                SourceText.From(source, Encoding.UTF8)
            );
        }
    }

    private static void ExtractGenericPartDetails(
        IMethodSymbol methodSymbol,
        MethodDeclarationSyntax methodDeclaration,
        out string? generics,
        out string? constraints
    )
    {
        generics = null;
        constraints = null;

        var genericTypeArguments = methodSymbol.TypeArguments;
        if (genericTypeArguments.Length == 0)
        {
            return;
        }

        generics = $"<{string.Join(", ", genericTypeArguments.Select(t => t.ToDisplayString()))}>";
        var parts = methodDeclaration.ConstraintClauses.Select(x => x.ToString());
        constraints = string.Join("\n", parts);
    }

    private static string BuildSafeStructName(string name, string? parameterType)
    {
        return parameterType == null ? name : $"{name}{parameterType}";
    }
}
