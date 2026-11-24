#pragma warning disable MA0051

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Firebreak;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Pasted;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Tuxedo.SourceGenerator;

/// <summary>
/// Primary source generator that builds refined types from predicate methods
/// </summary>
[Generator]
public sealed partial class RefinementSourceGenerator : IIncrementalGenerator
{
#pragma warning disable RS2008
    private static readonly DiagnosticDescriptor RefinementBuildError = new DiagnosticDescriptor(
        "TUXEDO001",
        "Failed to build refinement details",
        "{0}",
        "Tuxedo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
#pragma warning restore RS2008

    [ThreadStatic]
    private static StringBuilder? _stringBuilder;
    private static StringBuilder StringBuilder
    {
        get
        {
            if (_stringBuilder is null)
            {
                _stringBuilder = new StringBuilder();
            }
            else
            {
                _stringBuilder.Clear();
            }
            return _stringBuilder;
        }
    }

    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource("RefinementAttribute.g", EmbeddedFiles.RefinementAttribute_Source);
            ctx.AddSource("RefinedTypeAttribute.g", EmbeddedFiles.RefinedTypeAttribute_Source);
        });

        var refinedTypeDetailsProvider = context.SyntaxProvider.ForAttributeWithMetadataName(
            "Tuxedo.RefinementAttribute",
            IsRefinementMethod,
            BuildRefinedTypeDetailsSafe
        );

        context.RegisterSourceOutput(refinedTypeDetailsProvider, GenerateRefinedTypes);
        context.RegisterSourceOutput(
            refinedTypeDetailsProvider.Collect(),
            GenerateRefinementService
        );
    }

    private static bool IsRefinementMethod(SyntaxNode s, CancellationToken cancellationToken)
    {
        return s is MethodDeclarationSyntax mds
            // the method is either static or within the struct we are refining
            && IsStaticOrWithinGeneratedType(mds)
            // and returns a bool or a string
            && (
                string.Equals(mds.ReturnType.ToString(), "bool", StringComparison.Ordinal)
                || string.Equals(mds.ReturnType.ToString(), "string?", StringComparison.Ordinal)
            )
            // and has at least one parameter and no more than two
            && mds.ParameterList.Parameters.Count is >= 1 and <= 2
            // and if there are two parameters, the second is an out parameter
            && (
                mds.ParameterList.Parameters.Count == 1
                || mds.ParameterList.Parameters[1]
                    .Modifiers.Any(m => m.IsKind(SyntaxKind.OutKeyword))
            );
    }

    private static bool IsStaticOrWithinGeneratedType(MethodDeclarationSyntax mds)
    {
        if (mds.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)))
        {
            return true;
        }

        // Only parse the attribute when we need it. Bad attributes are caught by
        // the Firebreak stage wrapper in BuildRefinedTypeDetailsSafe.
        var attributeParts = new RefinementAttributeParts(mds);
        return mds.Ancestors()
            .OfType<StructDeclarationSyntax>()
            .Any(tds =>
                string.Equals(tds.Identifier.Text, attributeParts.Name, StringComparison.Ordinal)
            );
    }

    private static Result<RefinedTypeDetails> BuildRefinedTypeDetailsSafe(
        GeneratorAttributeSyntaxContext ctx,
        CancellationToken token
    )
    {
        // Firebreak POC: a bad refinement attribute is a located Err item, not
        // an AD0001 generator crash.
        return FirebreakPipeline.Capture(
            () => BuildRefinedTypeDetails(ctx, token).Unwrap(),
            ctx.TargetNode,
            RefinementBuildError
        );
    }

    private static Result<RefinedTypeDetails> BuildRefinedTypeDetails(
        GeneratorAttributeSyntaxContext ctx,
        CancellationToken token
    )
    {
        var methodDeclarationSyntax = (MethodDeclarationSyntax)ctx.TargetNode;

        // get the namespace
        var methodSymbol = ctx.SemanticModel.GetDeclaredSymbol(
            methodDeclarationSyntax,
            cancellationToken: token
        )!;
        var ns = methodSymbol.ContainingNamespace.ToDisplayString();

        // is it returning a string or a bool?
        var returnType = methodDeclarationSyntax.ReturnType.ToString();
        var returningFailureMessage = !string.Equals(returnType, "bool", StringComparison.Ordinal);

        // get all usings
        var usings = methodDeclarationSyntax
            .TryGetUsings()
            .Add(SF.UsingDirective(SF.ParseName(" System")))
            .Add(SF.UsingDirective(SF.ParseName(" System.Diagnostics.CodeAnalysis")))
            .Add(SF.UsingDirective(SF.ParseName(" Tuxedo")));

        // get the predicate details
        var containingType = methodSymbol.ContainingType;
        var @class = containingType.ToDisplayString();
        var name = methodDeclarationSyntax.Identifier.Text;
        var isStatic = methodSymbol.IsStatic;

        // get the attribute details
        var attributeParts = new RefinementAttributeParts(methodDeclarationSyntax);

        if (attributeParts.Name is not null && !IsValidIdentifier(attributeParts.Name))
        {
            return new Err<RefinedTypeDetails>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        RefinementBuildError,
                        "The Name argument on the Refinement attribute must be a valid C# identifier.",
                        ctx.TargetNode
                    )
                )
            );
        }

        // extract the generic parts
        var genericDetails = ExtractGenericPartDetails(methodSymbol, methodDeclarationSyntax);

        if (methodDeclarationSyntax.ParameterList.Parameters.Count == 0)
        {
            return new Err<RefinedTypeDetails>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        RefinementBuildError,
                        "Refinement methods must have at least one parameter.",
                        ctx.TargetNode
                    )
                )
            );
        }

        // get the first parameter type
        var firstParam = methodDeclarationSyntax.ParameterList.Parameters[0].Type!;
        var firstParameterTypeInfo = ctx
            .SemanticModel.GetTypeInfo(firstParam, cancellationToken: token)
            .Type;

        if (firstParameterTypeInfo is null)
        {
            return new Err<RefinedTypeDetails>(
                EquatableArray<GenerationError>.Create(
                    GenerationError.At(
                        RefinementBuildError,
                        "Could not resolve the first refinement parameter type.",
                        ctx.TargetNode
                    )
                )
            );
        }
        var rawType = firstParameterTypeInfo!.ToDisplayString();

        // build the name of the refined type
        var refinedType = BuildSafeStructName(
            attributeParts.Name ?? name,
            attributeParts.Name is not null
                ? null
                : rawType.UppercaseFirst()?.RemoveNamespace().RemoveGenerics()
        );
        var predicate = isStatic
            ? $"{@class}.{name}"
            : $"default({refinedType}{genericDetails?.Parameters}).{name}";

        // try and see if there is a second out parameter
        string? altType = null;
        ITypeSymbol? altTypeSymbol = null;
        if (methodDeclarationSyntax.ParameterList.Parameters.Count > 1)
        {
            var secondParam = methodDeclarationSyntax.ParameterList.Parameters[1].Type!;
            var secondParameterTypeInfo = ctx
                .SemanticModel.GetTypeInfo(secondParam, cancellationToken: token)
                .Type;
            altType = secondParameterTypeInfo!.ToDisplayString();
            altTypeSymbol = secondParameterTypeInfo;
        }

        return new Ok<RefinedTypeDetails>(
            new RefinedTypeDetails(
                Namespace: ns,
                Usings: usings,
                PredicateDetails: new PredicateDetails(
                    Name: predicate,
                    MethodDeclaration: methodDeclarationSyntax,
                    MethodSymbol: methodSymbol,
                    ReturnsFailureMessage: returningFailureMessage
                ),
                AttributeDetails: attributeParts,
                GenericDetails: genericDetails,
                RawType: rawType,
                RawTypeSymbol: firstParameterTypeInfo,
                RefinedType: refinedType,
                AlternativeType: altType,
                AlternativeTypeSymbol: altTypeSymbol
            )
        );
    }

    private static GenericPartDetails? ExtractGenericPartDetails(
        IMethodSymbol methodSymbol,
        MethodDeclarationSyntax methodDeclaration
    )
    {
        var genericTypeArguments = methodSymbol.TypeArguments;
        if (genericTypeArguments.Length != 0)
        {
            return new GenericPartDetails(
                ParameterSymbols: genericTypeArguments,
                ConstraintSyntaxes: methodDeclaration.ConstraintClauses
            );
        }

        // if the method has no generic type arguments, try to extract them from the enclosing type
        // this allows for generic constraints to be inherited from the containing class
        var enclosingType = methodSymbol.ContainingType;
        if (enclosingType is null)
        {
            return null;
        }

        genericTypeArguments = enclosingType.TypeArguments;
        if (genericTypeArguments.Length == 0)
        {
            return null;
        }

        return new GenericPartDetails(
            ParameterSymbols: genericTypeArguments,
            // extract constraints from the enclosing type's declaration syntax
            ConstraintSyntaxes: new SyntaxList<TypeParameterConstraintClauseSyntax>(
                methodDeclaration
                    .Ancestors()
                    .OfType<TypeDeclarationSyntax>()
                    .SelectMany(x => x.ConstraintClauses)
            )
        );
    }

    private static string BuildSafeStructName(string name, string? parameterType)
    {
        return parameterType == null ? name : $"{name}{parameterType}";
    }

    private static bool IsValidIdentifier(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (!char.IsLetter(value[0]) && value[0] != '_')
        {
            return false;
        }

        for (int i = 1; i < value.Length; i++)
        {
            if (!char.IsLetterOrDigit(value[i]) && value[i] != '_')
            {
                return false;
            }
        }

        return true;
    }

    private static void GenerateRefinedTypes(
        SourceProductionContext context,
        Result<RefinedTypeDetails> result
    )
    {
        if (result is Err<RefinedTypeDetails>)
        {
            context.ReportDiagnostics(result);
            return;
        }

        var refinedTypeDetails = ((Ok<RefinedTypeDetails>)result).Value;
        var stringBuilder = StringBuilder;
        if (refinedTypeDetails.AlternativeType is not null)
        {
            stringBuilder.WriteMultiRefinedType(refinedTypeDetails);
        }
        else
        {
            stringBuilder.WriteSingleRefinedType(refinedTypeDetails);
        }
        context.AddSource(
            $"{refinedTypeDetails.RefinedType}.g.cs",
            SourceText.From(stringBuilder.ToString(), Encoding.UTF8)
        );
    }

    private static void GenerateRefinementService(
        SourceProductionContext context,
        ImmutableArray<Result<RefinedTypeDetails>> results
    )
    {
        var details = new List<RefinedTypeDetails>();
        foreach (var result in results)
        {
            if (result is Ok<RefinedTypeDetails> ok)
            {
                details.Add(ok.Value);
            }
        }

        if (details.Count == 0)
        {
            return;
        }

        var stringBuilder = StringBuilder;
        stringBuilder.WriteRefinementService(details.ToImmutableArray());
        context.AddSource(
            "RefinementService.g.cs",
            SourceText.From(stringBuilder.ToString(), Encoding.UTF8)
        );
    }
}
