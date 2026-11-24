using System;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Tuxedo.SourceGenerator;
using Xunit;

namespace Tuxedo.Tests;

public sealed class FirebreakTests
{
    private static GeneratorDriverRunResult Run(string source)
    {
        var compilation = CSharpCompilation.Create(
            "firebreak",
            new[] { CSharpSyntaxTree.ParseText(source) },
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(RefinementAttribute).Assembly.Location),
            },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        var driver = CSharpGeneratorDriver.Create(new RefinementSourceGenerator());
        return driver.RunGenerators(compilation).GetRunResult();
    }

    [Fact]
    public void BadRefinementAttributeReportsLocatedDiagnosticInsteadOfCrash()
    {
        var result = Run(
            """
            using Tuxedo;

            internal static class Test
            {
                [Refinement("bad", Name = "Bad Type")]
                internal static bool Bad(bool value) => value;
            }
            """
        );

        var diagnostic = result.Diagnostics.FirstOrDefault(d =>
            d.Id.Equals("TUXEDO001", StringComparison.Ordinal)
        );
        Assert.True(
            diagnostic is not null,
            $"diagnostics={result.Diagnostics.Count()}; generated={result.GeneratedTrees.Length}"
        );
        Assert.Equal(DiagnosticSeverity.Error, diagnostic!.Severity);
        Assert.NotEqual(Location.None, diagnostic!.Location);
        Assert.DoesNotContain(
            result.Diagnostics,
            d => d.Id.Equals("AD0001", StringComparison.Ordinal)
        );
    }

    [Fact]
    public void GoodRefinementStillGeneratesWhenSiblingRefinementFails()
    {
        var result = Run(
            """
            using Tuxedo;

            internal static class Test
            {
                [Refinement("ok", Name = "Good")]
                internal static bool Good(bool value) => value;

                [Refinement("bad", Name = "Bad Type")]
                internal static bool Bad(bool value) => value;
            }
            """
        );

        var goodTree = result.GeneratedTrees.FirstOrDefault(t =>
            t.ToString().Contains("struct Good", StringComparison.Ordinal)
        );
        Assert.NotNull(goodTree);
        Assert.DoesNotContain(
            result.Diagnostics,
            d => d.Id.Equals("AD0001", StringComparison.Ordinal)
        );
        Assert.Equal(
            1,
            result.Diagnostics.Count(d => d.Id.Equals("TUXEDO001", StringComparison.Ordinal))
        );
    }
}
