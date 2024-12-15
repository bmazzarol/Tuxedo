using FluentAssertions;
using Tuxedo.Constants;
using Tuxedo.Types;
using Xunit;

namespace Tuxedo.Tests;

public sealed class UriStringTests
{
    [Fact(DisplayName = "A valid URI string can be refined")]
    public void Case1()
    {
        var refined = (UriString<AbsoluteUriKind>)"https://example.com";
        refined.RawValue.Should().Be("https://example.com");
        refined.RefinedValue.Should().Be(new Uri("https://example.com"));

        string value = refined;
        value.Should().Be("https://example.com");

        Uri value2 = refined;
        value2.Should().Be(new Uri("https://example.com"));

        if (refined is { RawValue: "https://example.com" }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        if (refined is { RefinedValue: { } }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        var (a, b) = refined;

        a.Should().Be("https://example.com");
        b.Should().Be(new Uri("https://example.com"));

        Refined
            .TryRefine<UriString<AbsoluteUriKind>, string, Uri>(
                "https://example.com",
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.RawValue.Should().Be("https://example.com");
        refinedValue.RefinedValue.Should().Be(new Uri("https://example.com"));
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "An invalid URI string cannot be refined")]
    public void Case2()
    {
        var op = () => (UriString<AbsoluteUriKind>)"invalid";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid Uri, but was 'invalid'")
            .Which.Value.Should()
            .Be("invalid");
    }

    [Fact(DisplayName = "A valid URI string can be refined with a relative URI kind")]
    public void Case3()
    {
        var refined = (UriString<RelativeUriKind>)"/path/to/resource";
        refined.RawValue.Should().Be("/path/to/resource");
        refined.RefinedValue.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        string value = refined;
        value.Should().Be("/path/to/resource");

        Uri value2 = refined;
        value2.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        if (refined is { RawValue: "/path/to/resource" }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        if (refined is { RefinedValue: { } }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        var (a, b) = refined;

        a.Should().Be("/path/to/resource");
        b.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        Refined
            .TryRefine<UriString<RelativeUriKind>, string, Uri>(
                "/path/to/resource",
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.RawValue.Should().Be("/path/to/resource");
        refinedValue.RefinedValue.Should().Be(new Uri("/path/to/resource", UriKind.Relative));
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A valid URI string can be refined with a relative or absolute URI kind")]
    public void Case4()
    {
        var refined = (UriString<AbsoluteOrRelativeUriKind>)"/path/to/resource";
        refined.RawValue.Should().Be("/path/to/resource");
        refined.RefinedValue.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        string value = refined;
        value.Should().Be("/path/to/resource");

        Uri value2 = refined;
        value2.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        if (refined is { RawValue: "/path/to/resource" }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        if (refined is { RefinedValue: { } }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        var (a, b) = refined;

        a.Should().Be("/path/to/resource");
        b.Should().Be(new Uri("/path/to/resource", UriKind.Relative));

        Refined
            .TryRefine<UriString<AbsoluteOrRelativeUriKind>, string, Uri>(
                "/path/to/resource",
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.RawValue.Should().Be("/path/to/resource");
        refinedValue.RefinedValue.Should().Be(new Uri("/path/to/resource", UriKind.Relative));
        failureMessage.Should().BeNull();
    }
}
