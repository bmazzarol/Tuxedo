using FluentAssertions;
using Xunit;

namespace Tuxedo.Tests;

public static class CustomAccessModifier
{
    [Refinement("`{value}` is not a whitespace character", isPublic: false)]
    public static bool IsWhiteSpace(char value) => char.IsWhiteSpace(value);
}

public sealed class CustomAccessModifierTests
{
    private static string AppendWhiteSpace(char value, Refined<char, IsWhiteSpace> ws) =>
        $"{value} {ws.Value}";

    [Fact(DisplayName = "Internal refinements can be used in the same assembly")]
    public void Case1()
    {
        const char value = ' ';
        Refined<char, IsWhiteSpace> ws = value;
        AppendWhiteSpace(value, ws).Should().Be("   ");
        typeof(IsWhiteSpace).IsPublic.Should().BeFalse();
    }
}
