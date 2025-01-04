using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Tuxedo.SourceGenerator;

namespace Tuxedo.Tests.Extensions;

public static class GeneratorDriverExtensions
{
    internal static GeneratorDriver BuildDriver(this string? source)
    {
        var compilation = CSharpCompilation.Create(
            "name",
            source != null ? [CSharpSyntaxTree.ParseText(source)] : []
        );
        var generator = new RefinementSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }

    internal static Task VerifyRefinement(
        this string? source,
        [CallerFilePath] string sourceFile = ""
    )
    {
        var driver = $$"""
            using Tuxedo;

            internal static class Test
            {
               {{source}}
            }
            """.BuildDriver();
        return Verify(driver, sourceFile: sourceFile)
            .IgnoreGeneratedResult(x =>
                x.HintName
                    is "RefinementAttribute.g.cs"
                        or "RefinedTypeAttribute.g.cs"
                        or "RefinementService.g.cs"
            );
    }
}
