using FluentAssertions;
using Xunit;

namespace Tuxedo.Tests;

public static class CustomAccessModifier
{
    [Refinement(
        "`{value}` is not a whitespace character",
        isInternal: true,
        dropTypeFromName: false
    )]
    public static bool WhiteSpace(char value) => char.IsWhiteSpace(value);
}

public sealed class CustomAccessModifierTests
{
    private static string AppendWhiteSpace(char value, WhiteSpaceChar ws) => $"{value} {ws.Value}";

    [Fact(DisplayName = "Internal refinements can be used in the same assembly")]
    public void Case1()
    {
        const char value = ' ';
        var ws = (WhiteSpaceChar)value;
        AppendWhiteSpace(value, ws).Should().Be("   ");
        typeof(WhiteSpaceChar).IsPublic.Should().BeFalse();
    }
}
