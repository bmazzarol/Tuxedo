using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Tuxedo.SourceGenerator;

internal static class SyntaxExtensions
{
    public static SyntaxList<UsingDirectiveSyntax> TryGetUsings(this SyntaxNode node)
    {
        return node.Ancestors(ascendOutOfTrivia: false)
            .Aggregate(
                SyntaxFactory.List<UsingDirectiveSyntax>(),
                (current, parent) =>
                    parent switch
                    {
                        NamespaceDeclarationSyntax syntax => current.AddRange(syntax.Usings),
                        CompilationUnitSyntax syntax => current.AddRange(syntax.Usings),
                        _ => current,
                    }
            );
    }
}
