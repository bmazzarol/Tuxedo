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
            source != null ? [CSharpSyntaxTree.ParseText(source)] : [],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(RefinementAttribute).Assembly.Location),
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
        var generator = new RefinementSourceGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }

    internal static SettingsTask IgnoreStandardSupportCode(this SettingsTask settings)
    {
        return settings.IgnoreGeneratedResult(x =>
            x.HintName
                is "RefinementAttribute.g.cs"
                    or "RefinedTypeAttribute.g.cs"
                    or "RefinementService.g.cs"
        );
    }

    internal static Task VerifyRefinement(
        this string? source,
        [CallerFilePath] string sourceFile = ""
    )
    {
        var driver = $$"""
            using System;
            using Tuxedo;

            internal static class Test
            {
               {{source}}
            }
            """.BuildDriver();
        return Verify(driver, sourceFile: sourceFile).IgnoreStandardSupportCode();
    }
}
