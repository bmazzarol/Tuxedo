using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

public static class CustomAccessModifier
{
    [Refinement("`{value}` is not a whitespace character", IsInternal = true)]
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
        Assert.Equal("   ", AppendWhiteSpace(value, ws));
        Assert.False(typeof(WhiteSpaceChar).IsPublic);
    }

    [Fact(DisplayName = "WhiteSpace refinement snapshot is correct and should be internal")]
    public Task Case2()
    {
        return """
            [Refinement("`{value}` is not a whitespace character", IsInternal = true)]
            public static bool WhiteSpace(char value) => char.IsWhiteSpace(value);
            """.VerifyRefinement();
    }
}
