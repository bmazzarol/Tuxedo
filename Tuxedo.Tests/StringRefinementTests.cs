﻿using System.Text.RegularExpressions;

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
        Refined<string, Not<string, NonEmpty>> refined = string.Empty;
        refined.Value.Should().BeEmpty();
        Refined.TryRefine<string, Not<string, NonEmpty>>(string.Empty, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A trimmed string can be refined")]
    public static void Case6()
    {
        Refined<string, Trimmed> trimmedString = "Hello, World!";
        ((string)trimmedString).Should().Be("Hello, World!");
        string.Equals(trimmedString, "Hello, World!", StringComparison.Ordinal).Should().BeTrue();
        Refined.TryRefine<string, Trimmed>("Hello, World!", out _).Should().BeTrue();

        // leading and trailing whitespace is removed by the refinement
        Refined<string, string, Trimmed> trimmedString2 = " Hello, World! ";
        trimmedString2.RefinedValue.Should().Be("Hello, World!");
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
        var act = () => (Refined<string, Trimmed>)" Hello, World! ";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be trimmed, but found ' Hello, World! '")
            .And.Value.Should()
            .Be(" Hello, World! ");
    }

    [Fact(DisplayName = "A refinement can be combined with logical AND")]
    public static void Case10()
    {
        Refined<string, And<string, NonEmpty, Trimmed>> refined = "Hello, World!";
        ((string)refined).Should().Be("Hello, World!");
        Refined
            .TryRefine<string, And<string, NonEmpty, Trimmed>>("Hello, World!", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A refinement can be combined with logical AND and throws")]
    public static void Case11()
    {
        var act = () => (Refined<string, And<string, NonEmpty, Trimmed>>)" Hello, World! ";
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be trimmed, but found ' Hello, World! '")
            .And.Value.Should()
            .Be(" Hello, World! ");
    }

    [Fact(DisplayName = "A string guid can be refined")]
    public static void Case12()
    {
        Refined<string, Guid, Uuid> refined = "d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a";
        var (raw, guid) = refined;
        raw.Should().Be("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a");
        guid.Should().Be(Guid.Parse("d3f4e5a6-7b8c-9d0e-1f2a-3b4c5d6e7f8a"));
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
        Refined<string, Uri<AbsoluteKind>> refined = "https://www.bmazzarol.com.au";
        ((string)refined).Should().Be("https://www.bmazzarol.com.au");
        Refined
            .TryRefine<string, Uri<AbsoluteKind>>("https://www.bmazzarol.com.au", out _)
            .Should()
            .BeTrue();

        Refined<string, System.Uri, Uri<AbsoluteKind>> refined2 = "http://www.bmazzarol.com.au";
        ((string)refined2).Should().Be("http://www.bmazzarol.com.au");
        ((System.Uri)refined2)
            .Should()
            .Be(new System.Uri("http://www.bmazzarol.com.au", UriKind.Absolute));
        Refined
            .TryRefine<string, System.Uri, Uri<AbsoluteKind>>("http://www.bmazzarol.com.au", out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A relative uri can be refined")]
    public static void Case16()
    {
        Refined<string, Uri<RelativeKind>> refined = "/some/path";
        ((string)refined).Should().Be("/some/path");
        Refined.TryRefine<string, Uri<RelativeKind>>("/some/path", out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A relative uri cannot be refined and throws")]
    public static void Case17()
    {
        var act = () => (Refined<string, Uri<RelativeKind>>)"https://www.bmazzarol.com.au";
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
        Refined<string, Uri> refined = uri;
        ((string)refined).Should().Be(uri);
        Refined.TryRefine<string, Uri>(uri, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A non uri cannot be refined and throws")]
    public static void Case19()
    {
        // invalid uri characters
        var act = () => (Refined<string, Uri>)null!;
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
        RefinementFailureException? e = null;

        try
        {
            Refined<string, StartsWith<HelloPrefix>> _ = "Hi, World!";
        }
        catch (RefinementFailureException ex)
        {
            e = ex;
        }

        e!.Message.Should().Be("Value must start with 'Hello' but started with 'Hi, W'");
        e.Value.Should().Be("Hi, World!");
        e.StackTrace.Should().StartWith("   at Tuxedo.Tests.StringRefinementTests.Case23()");
    }

    [Fact(DisplayName = "An empty string can be refined using empty")]
    public static void Case24()
    {
        Refined<string, Empty> emptyString = string.Empty;
        emptyString.Value.Should().BeEmpty();
        Refined.TryRefine<string, Empty>(string.Empty, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "An null string can be refined using empty")]
    public static void Case25()
    {
        Refined<string, Empty> emptyString = null!;
        emptyString.Value.Should().BeNull();
        Refined.TryRefine<string, Empty>(null!, out _).Should().BeTrue();
    }

    [Fact(DisplayName = "A string can be refined by size")]
    public static void Case26()
    {
        Refined<string, Size<Even>> refined = "123456";
        ((string)refined).Should().Be("123456");
    }

    [Fact(DisplayName = "A string cannot be refined by size")]
    public static void Case27()
    {
        Refined.TryRefine<string, Size<Even>>("12345", out _).Should().BeFalse();
    }
}
