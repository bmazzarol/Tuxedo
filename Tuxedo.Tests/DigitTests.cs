namespace Tuxedo.Tests;

public static class DigitTests
{
    [Fact(DisplayName = "Characters can be refined as digits")]
    public static void Case1()
    {
        Refined<char, Digit> digit = '1';
        digit.Value.Should().Be('1');
        char raw = digit;
        raw.Should().Be('1');
    }

    [Fact(DisplayName = "Characters that are not digits cannot be refined")]
    public static void Case2()
    {
        Action act = () => _ = (Refined<char, Digit>)'a';
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a digit, instead found 'a'")
            .And.StackTrace.Should()
            .StartWith("   at Tuxedo.Tests.DigitTests");
    }
}
