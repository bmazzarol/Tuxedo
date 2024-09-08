using FluentAssertions;
using Xunit;

namespace Tuxedo.Tests;

public static class GenericRefinements
{
    [Refinement("The value must be '{default(TOther).Value}', instead found '{value}'")]
    internal static bool Equal<T, TOther>(T value)
        where TOther : struct, IConstant<TOther, T> => Equals(value, default(TOther).Value);
}

public readonly record struct FortyTwo : IConstant<FortyTwo, int>
{
    public int Value => 42;
}

public sealed class GenericRefinementTests
{
    [Fact(DisplayName = "A value can be refined to a constant value")]
    public void Case1()
    {
        const int value = 42;
        Refined<int, Equal<int, FortyTwo>> refined = value;
        refined.Value.Should().Be(42);
    }

    [Fact(
        DisplayName = "A value that is not equal to the constant value should fail the refinement"
    )]
    public void Case2()
    {
        const int value = 43;
        Assert
            .Throws<RefinementFailureException>(() => (Refined<int, Equal<int, FortyTwo>>)value)
            .Message.Should()
            .Be("The value must be '42', instead found '43'");
        Refined.TryRefine(value, out Refined<int, Equal<int, FortyTwo>> _).Should().BeFalse();
    }
}
