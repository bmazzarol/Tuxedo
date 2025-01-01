using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Tuxedo.SourceGenerator.Analysers;
using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

internal static class BoolRefinements
{
    [Refinement("The boolean value must be 'True', instead found '{value}'")]
    internal static bool True(bool value) => value;

    [Refinement("The boolean value must be 'False', instead found '{value}'")]
    internal static bool False(bool value) => !value;
}

public class BoolRefinementsTests
{
    [Fact(DisplayName = "A boolean value can be refined to True")]
    public void Case1()
    {
        const bool value = true;
        var refined = (TrueBool)value;
        refined.Value.Should().BeTrue();
    }

    [Fact(DisplayName = "A boolean value can be refined to False")]
    public void Case2()
    {
        const bool value = false;
        var refined = (FalseBool)value;
        refined.Value.Should().BeFalse();
    }

    [Fact(DisplayName = "A False refinement should fail when the value is True")]
    public void Case3()
    {
        const bool value = true;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => (FalseBool)value);
        ex.Message.Should().StartWith("The boolean value must be 'False', instead found 'True'");
        ex.ActualValue.Should().Be(value);
        ex.ParamName.Should().Be("value");
        FalseBool.TryParse(value, out _, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "FalseBool refinement snapshot is correct")]
    public Task Case4()
    {
        return """
            [Refinement("The boolean value must be 'False', instead found '{value}'")]
            internal static bool False(bool value) => !value;
            """.VerifyRefinement();
    }

    [Fact(DisplayName = "TrueBool refinement snapshot is correct")]
    public Task Case5()
    {
        return """
            [Refinement("The boolean value must be 'True', instead found '{value}'")]
            internal static bool True(bool value) => value;
            """.VerifyRefinement();
    }

    [Fact(DisplayName = "TrueBool cannot be assigned default")]
    public Task Case6()
    {
        var context = new CSharpAnalyzerTest<
            DoNotUseDefaultRefinedTypeInstanceAnalyzer,
            DefaultVerifier
        >
        {
            ReferenceAssemblies = References.Net8AndOurs.Value,
            TestCode = """
                using System;

                namespace TestProject;

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
    public Task Case67()
    {
        var context = new CSharpAnalyzerTest<
            DoNotUseNewRefinedTypeInstanceAnalyzer,
            DefaultVerifier
        >
        {
            ReferenceAssemblies = References.Net8AndOurs.Value,
            TestCode = """
                using System;

                namespace TestProject;

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
}
