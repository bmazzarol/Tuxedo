using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerator;

internal sealed record PredicateDetails(
    string? Name,
    MethodDeclarationSyntax MethodDeclaration,
    IMethodSymbol MethodSymbol,
    bool ReturnsFailureMessage
);
