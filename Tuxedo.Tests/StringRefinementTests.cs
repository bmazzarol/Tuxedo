using System.Text.RegularExpressions;

namespace Tuxedo.Tests;

public static class StringRefinementTests
{
    [Fact(DisplayName = "A non-empty string can be refined")]
    public static void Case1()
    {
        Refined<string, NonEmpty> nonEmptyString = "Hello, World!";
        ((string)nonEmptyString).Should().Be("Hello, World!");
        string.Equals(nonEmptyString, "Hello, World!", StringComparison.Ordinal).Should().BeTrue();
        Refined.TryRefine<string, NonEmpty>("Hello, World!", out _).Should().BeTrue();
    }

    [Fact(DisplayName = "An empty string cannot be refined")]
    public static void Case2()
    {
        Refined.TryRefine<string, NonEmpty>(string.Empty, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A null string cannot be refined")]
    public static void Case3()
    {
        Refined.TryRefine<string, NonEmpty>(null!, out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A empty string cannot be refined and throws")]
    public static void Case4()
    {
        var act = () => (Refined<string, NonEmpty>)string.Empty;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value cannot be empty")
            .And.Value.Should()
            .Be(string.Empty);
    }

    [Fact(DisplayName = "A non-empty string refinement can be inverted")]
    public static void Case5()
    {
        Refined<string, Not<NonEmpty, string>> refined = string.Empty;
        ((string)refined).Should().BeEmpty();
        Refined.TryRefine<string, Not<NonEmpty, string>>(string.Empty, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A trimmed string can be refined")]
    public static void Case6()
    {
        Refined<string, Trimmed> trimmedString = "Hello, World!";
        ((string)trimmedString).Should().Be("Hello, World!");
        string.Equals(trimmedString, "Hello, World!", StringComparison.Ordinal).Should().BeTrue();
        Refined.TryRefine<string, Trimmed>("Hello, World!", out _).Should().BeTrue();

        // leading and trailing whitespace is removed by the refinement
        var trimmedString2 = (Refined<string, Trimmed>)" Hello, World! ";
        ((string)trimmedString2).Should().Be("Hello, World!");
    }

    [Fact(DisplayName = "A string with leading whitespace cannot be refined")]
    public static void Case7()
    {
        Refined.TryRefine<string, Trimmed>(" Hello, World!", out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A string with trailing whitespace cannot be refined")]
    public static void Case8()
    {
        Refined.TryRefine<string, Trimmed>("Hello, World! ", out _).Should().BeFalse();
    }

    [Fact(
        DisplayName = "A string with leading and trailing whitespace cannot be refined and throws"
    )]
    public static void Case9()
    {
        var act = () =>
            Refined.Refine<string, Trimmed>(" Hello, World! ", tryApplyRefinement: false);
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must have no leading or trailing whitespace")
            .And.Value.Should()
            .Be(" Hello, World! ");
    }

    [Fact(DisplayName = "A refinement can be combined with logical AND")]
    public static void Case10()
    {
        Refined<string, And<NonEmpty, Trimmed, string>> refined = "Hello, World!";
        ((string)refined).Should().Be("Hello, World!");
        Refined
            .TryRefine<string, And<NonEmpty, Trimmed, string>>("Hello, World!", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A refinement can be combined with logical AND and throws")]
    public static void Case11()
    {
        var act = () => (Refined<string, And<NonEmpty, Trimmed, string>>)" Hello, World! ";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage(
                "Value cannot be empty and Value must have no leading or trailing whitespace"
            )
            .And.Value.Should()
            .Be(" Hello, World! ");
    }

    [Fact(DisplayName = "A string guid can be refined")]
    public static void Case12()
    {
        Refined<string, Guid, Uuid> refined = "d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a";
        ((string)refined).Should().Be("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a");
        ((Guid)refined).Should().Be(Guid.Parse("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a"));
        Refined
            .TryRefine<string, Guid, Uuid>("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A string guid cannot be refined and throws")]
    public static void Case13()
    {
        var act = () => (Refined<string, Uuid>)"d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a9";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid GUID")
            .And.Value.Should()
            .Be("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a9");
    }

    [Fact(DisplayName = "A absolute uri can be refined")]
    public static void Case14()
    {
        Refined<string, AbsoluteUri> refined = "https://www.bmazzarol.com.au";
        ((string)refined).Should().Be("https://www.bmazzarol.com.au");
        Refined
            .TryRefine<string, AbsoluteUri>("https://www.bmazzarol.com.au", out _)
            .Should()
            .BeTrue();

        Refined<string, Uri, AbsoluteUri> refined2 = "http://www.bmazzarol.com.au";
        ((string)refined2).Should().Be("http://www.bmazzarol.com.au");
        ((Uri)refined2).Should().Be(new Uri("http://www.bmazzarol.com.au", UriKind.Absolute));
        Refined
            .TryRefine<string, Uri, AbsoluteUri>("http://www.bmazzarol.com.au", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A absolute uri cannot be refined and throws")]
    public static void Case15()
    {
        var act = () => (Refined<string, AbsoluteUri>)"/some/path";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid absolute URI")
            .And.Value.Should()
            .Be("/some/path");
        Refined.TryRefine<string, Uri, AbsoluteUri>("/some/path", out _).Should().BeFalse();
    }

    [Fact(DisplayName = "A relative uri can be refined")]
    public static void Case16()
    {
        Refined<string, RelativeUri> refined = "/some/path";
        ((string)refined).Should().Be("/some/path");
        Refined.TryRefine<string, RelativeUri>("/some/path", out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A relative uri cannot be refined and throws")]
    public static void Case17()
    {
        var act = () => (Refined<string, RelativeUri>)"https://www.bmazzarol.com.au";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid relative URI")
            .And.Value.Should()
            .Be("https://www.bmazzarol.com.au");
    }

    [Theory(DisplayName = "A relative or absolute uri can be refined")]
    [InlineData("/some/path")]
    [InlineData("https://www.bmazzarol.com.au")]
    public static void Case18(string uri)
    {
        Refined<string, AnyUri> refined = uri;
        ((string)refined).Should().Be(uri);
        Refined.TryRefine<string, AnyUri>(uri, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A non uri cannot be refined and throws")]
    public static void Case19()
    {
        // invalid uri characters
        var act = () => (Refined<string, AnyUri>)null!;
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid URI")
            .And.Value.Should()
            .BeNull();
    }

    private readonly struct ExampleRegex : IConstant<ExampleRegex, string>
    {
        public string Value => "^([1-9]{1})([0-9]{3})$";
    }

    [Fact(DisplayName = "A string can be refined using a regex type level constant")]
    public static void Case20()
    {
        Refined<string, MatchesRegex<ExampleRegex>> refined = "1234";
        ((string)refined).Should().Be("1234");
        Refined.TryRefine<string, MatchesRegex<ExampleRegex>>("1234", out _).Should().BeTrue();

        Refined<string, MatchCollection, MatchesRegex<ExampleRegex>> refined2 = "9234";
        ((string)refined2).Should().Be("9234");
        MatchCollection collection = refined2;
        collection
            .Should()
            .ContainSingle(m => m.Value == "9234")
            .And.Subject.First()
            .Groups.Values.Should()
            .HaveCount(3)
            .And.Contain(g => g.Value == "9")
            .And.Contain(g => g.Value == "234");
    }

    [Fact(DisplayName = "A string cannot be refined using a regex type level constant and throws")]
    public static void Case21()
    {
        var act = () => (Refined<string, MatchesRegex<ExampleRegex>>)"02345";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must match the regular expression '^([1-9]{1})([0-9]{3})$'")
            .And.Value.Should()
            .Be("02345");
    }

    private readonly struct HelloPrefix : IConstant<HelloPrefix, string>
    {
        public string Value => "Hello";
    }

    [Fact(DisplayName = "A string can be refined using a starts with refinement")]
    public static void Case22()
    {
        Refined<string, StartsWith<HelloPrefix>> refined = "Hello, World!";
        ((string)refined).Should().Be("Hello, World!");
        Refined
            .TryRefine<string, StartsWith<HelloPrefix>>("Hello, World!", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A string cannot be refined using a starts with refinement and throws")]
    public static void Case23()
    {
        var act = () => (Refined<string, StartsWith<HelloPrefix>>)"Hi, World!";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must start with 'Hello'")
            .And.Value.Should()
            .Be("Hi, World!");
    }
}
