using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

public sealed class ConfigureRefinementTypesExample
{
    [Refinement("Must be true", Name = nameof(Case1))]
    public static bool ExampleWithConstructorMessage(bool value) => value;

    [Refinement(FailureMessage = "Must be true", Name = nameof(Case2))]
    public static bool ExampleWithPropertyMessage(bool value) => value;

    [Refinement(Name = nameof(Case3))]
    public static string? ExampleWithReturnedFailureMessage(bool value) =>
        !value ? "Must be true" : null;

    [Fact(DisplayName = "A string returning refinement method renders correctly")]
    public Task Case1()
    {
        return """
            [Refinement(Name = "Case3")]
            public static string? ExampleWithReturnedFailureMessage(bool value) =>
                !value ? "Must be true" : null;
            """.VerifyRefinement();
    }
}
