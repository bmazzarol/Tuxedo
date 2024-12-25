using FluentAssertions;
using Tuxedo.Tests.CustomTypes;
using Tuxedo.Tests.Refinements;
using Xunit;

namespace Tuxedo.Tests;

public class CustomRefinedTypeExample
{
    [Fact(DisplayName = "A positive double can be refined")]
    public void Case1()
    {
        var refined = (PositiveDouble)1.345;
        refined.Value.Should().Be(1.345);

        double value = refined;
        value.Should().Be(1.345);

        if (refined is { Value: 1.345 }) { }
        else
        {
            throw new Xunit.Sdk.XunitException("Pattern match failed");
        }

        PositiveDouble
            .TryParse(1.345, out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Value.Should().Be(refined.Value);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A negative double cannot be refined")]
    public void Case2()
    {
        var op = () => (PositiveDouble)(-1.345);
        var failure = op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be positive, but was '-1.345'")
            .Which;
        failure.Value.Should().Be(-1.345);
        failure.ValueType.Should().Be<double>();
        PositiveDouble.TryParse(-1.345, out _, out var failureMessage).Should().BeFalse();
        failureMessage.Should().Be("Value must be positive, but was '-1.345'");
    }

    [Fact(DisplayName = "A string can be refined as double")]
    public void Case3()
    {
        var refined = (PositiveDoubleString)"1.345";
        refined.RawValue.Should().Be("1.345");
        refined.RefinedValue.Should().Be(1.345);

        PositiveDoubleString
            .TryParse("1.345", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A string can not be refined as double")]
    public void Case4()
    {
        var op = () => (PositiveDoubleString)"not a double";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be a valid Double, but was 'not a double'");
        PositiveDoubleString
            .TryParse("not a double", out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("Value must be a valid Double, but was 'not a double'");
    }

    [Fact(DisplayName = "A string can be refined as an item number")]
    public void Case5()
    {
        var refined = (ItemNumberString)"1423";
        refined.Value.Should().Be("1423");

        ItemNumberString
            .TryParse("1423", out var refinedValue, out var failureMessage)
            .Should()
            .BeTrue();
        refinedValue.Should().Be(refined);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A string can not be refined as an item number")]
    public void Case6()
    {
        var op = () => (ItemNumberString)"not an item number";
        op.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("'not an item number' is not a valid item number.");
        ItemNumberString
            .TryParse("not an item number", out _, out var failureMessage)
            .Should()
            .BeFalse();
        failureMessage.Should().Be("'not an item number' is not a valid item number.");
    }
}
