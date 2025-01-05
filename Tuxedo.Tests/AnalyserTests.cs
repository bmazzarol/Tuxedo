using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Tuxedo.SourceGenerator.Analysers;

namespace Tuxedo.Tests;

public sealed class AnalyserTests
{
    [Fact(DisplayName = "TrueBool cannot be assigned default")]
    public Task Case1()
    {
        var context = new CSharpAnalyzerTest<
            DoNotUseDefaultRefinedTypeInstanceAnalyzer,
            DefaultVerifier
        >
        {
            ReferenceAssemblies = References.Net8AndOurs.Value,
            TestCode = """
                using System;

                namespace Tuxedo;

                [AttributeUsage(AttributeTargets.Struct)]
                internal sealed class RefinedTypeAttribute : Attribute {}    

                [RefinedType]
                internal readonly partial struct TrueBool {}

                internal class Test
                {
                    public static void TestMethod()
                    {
                        TrueBool someValue = [|default|];
                        var someOtherValue = [|default(TrueBool)|];
                        // default here is fine
                        Guid someGuid = default;
                        var someOtherGuid = default(Guid);
                    }
                }
                """,
        };
        return context.RunAsync();
    }

    [Fact(DisplayName = "TrueBool cannot be assigned from new")]
    public Task Case2()
    {
        var context = new CSharpAnalyzerTest<
            DoNotUseNewRefinedTypeInstanceAnalyzer,
            DefaultVerifier
        >
        {
            ReferenceAssemblies = References.Net8AndOurs.Value,
            TestCode = """
                using System;

                namespace Tuxedo;

                [AttributeUsage(AttributeTargets.Struct)]
                internal sealed class RefinedTypeAttribute : Attribute {}    

                [RefinedType]
                internal readonly partial struct TrueBool {}

                internal class Test
                {
                    public static void TestMethod()
                    {
                        TrueBool someValue = [|new()|];
                        var someOtherValue = [|new TrueBool()|];
                        // this new is fine
                        Test someOkClass = new();
                        var someOtherOkClass = new Test();
                    }
                }
                """,
        };
        return context.RunAsync();
    }

    [Fact(DisplayName = "TrueBool cannot be assigned false")]
    public Task Case3()
    {
        var context = new CSharpAnalyzerTest<InvalidConstAssignmentAnalyser, DefaultVerifier>
        {
            ReferenceAssemblies = References.Net8AndOurs.Value,
            TestCode = """
                using System;

                namespace Tuxedo;

                [AttributeUsage(AttributeTargets.Struct)]
                internal sealed class RefinedTypeAttribute : Attribute {}    

                internal static class RefinementService
                {
                    private static string? TestAgainstTrueBool(object value)
                    {
                        return value is bool rt && !Tuxedo.TrueBool.TryParse(rt, out _, out var errorMessage) ? errorMessage : null;
                    }
                }

                [RefinedType]
                internal readonly partial struct TrueBool 
                {
                    public bool Value { get; }
                    
                    private TrueBool(bool value) => Value = value;
                    
                    public static explicit operator TrueBool(bool value)
                    {
                        return Parse(value);
                    }
                    
                    public static TrueBool Parse(bool value)
                    {
                        return value ? new TrueBool(value) : throw new ArgumentOutOfRangeException(nameof(value), value, "needs to be true");
                    }
                    
                    public static bool TryParse(bool value, out TrueBool result, out string error)
                    {
                        if(value)
                        {
                            result = new TrueBool(value);
                            error = null;
                            return true;
                        }
                        
                        result = default;
                        error = "needs to be true";
                        return false;
                    }
                }

                internal class Test
                {
                    public static void TestMethod()
                    {
                        // these are all bad
                        var bad1 = [|TrueBool.Parse(false)|];
                        var bad2 = [|TrueBool.TryParse(false, out _, out _)|];
                        var bad3 = [|(TrueBool)false|];
                        
                        const bool badConst = false;
                        var bad4 = [|TrueBool.Parse(badConst)|];
                        var bad5 = [|TrueBool.TryParse(badConst, out _, out _)|];
                        var bad6 = [|(TrueBool)badConst|];
                        
                        // these are fine
                        var ok1 = TrueBool.Parse(true);
                        var ok2 = (TrueBool)true;
                        var ok3 = TrueBool.TryParse(true, out _, out _);
                        // these unrelated parse methods are also fine
                        var ok4 = Guid.Parse("");
                        var ok5 = Guid.TryParse("", out _);
                        var ok6 = (double) 1;
                    }
                }
                """,
            DiagnosticVerifier = (diagnostic, _, _) =>
            {
                diagnostic.Severity.Should().Be(DiagnosticSeverity.Error);
                diagnostic.GetMessage().Should().Be("needs to be true");
            },
        };
        return context.RunAsync();
    }
}
