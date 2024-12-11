using FluentAssertions;
using Tuxedo.Refinements;
using Xunit;

namespace Tuxedo.Tests;

using ValidGuid = Refined<AsA<Guid>, string, Guid>;

public sealed class GuidStringRefinementTests
{
    [Fact(DisplayName = "A valid GUID string can be refined")]
    public void Case1()
    {
        ValidGuid refined = "00000000-0000-0000-0000-000000000000";
        refined.RawValue.Should().Be("00000000-0000-0000-0000-000000000000");
        refined.RefinedValue.Should().Be(new Guid("00000000-0000-0000-0000-000000000000"));

        string value = refined;
        value.Should().Be("00000000-0000-0000-0000-000000000000");

        Guid value2 = refined;
        value2.Should().Be(new Guid("00000000-0000-0000-0000-000000000000"));

        if (refined is { RawValue: "00000000-0000-0000-0000-000000000000" }) { }
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

        a.Should().Be("00000000-0000-0000-0000-000000000000");
        b.Should().Be(new Guid("00000000-0000-0000-0000-000000000000"));

        Refined
            .TryRefine<AsA<Guid>, string, Guid>(
                "00000000-0000-0000-0000-000000000000",
                out var refinedValue,
                out var failureMessage
            )
            .Should()
            .BeTrue();
        refinedValue.RawValue.Should().Be("00000000-0000-0000-0000-000000000000");
        refinedValue.RefinedValue.Should().Be(new Guid("00000000-0000-0000-0000-000000000000"));
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "An invalid GUID string cannot be refined")]
    public void Case2()
    {
        var op = () => (ValidGuid)("invalid");
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid Guid, but was 'invalid'")
            .Which.Value.Should()
            .Be("invalid");
        Refined
            .TryRefine<AsA<Guid>, string, Guid>("invalid", out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be a valid Guid, but was 'invalid'");
    }
}
