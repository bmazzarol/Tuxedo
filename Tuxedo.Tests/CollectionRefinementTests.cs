namespace Tuxedo.Tests;

public static class CollectionRefinementTests
{
    [Fact(DisplayName = "A non empty collection can be refined")]
    public static void Case1()
    {
        Refined<string[], NonEmpty<string[]>> nonEmptyCollection = new[] { "Hello", "World" };
        ((string[])nonEmptyCollection).Should().BeEquivalentTo("Hello", "World");
        Refined
            .TryRefine<string[], NonEmpty<string[]>>(new[] { "Hello", "World" }, out _)
            .Should()
            .BeTrue();
        nonEmptyCollection.Value[0].Should().Be("Hello");
    }

    [Fact(DisplayName = "An empty collection cannot be refined")]
    public static void Case2()
    {
        Refined
            .TryRefine<string[], NonEmpty<string[]>>(Array.Empty<string>(), out _)
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName = "An empty collection cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (Refined<string[], NonEmpty<string[]>>)Array.Empty<string>();
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value cannot be empty")
            .And.Value.Should()
            .BeOfType<string[]>()
            .Which.Should()
            .BeEmpty();
    }

    [Fact(DisplayName = "A non empty enumerable can be refined")]
    public static void Case4()
    {
        var nonEmptyCollection = Refined.Refine<IEnumerable<int>, NonEmpty<IEnumerable<int>>>(
            Enumerable.Range(1, 3)
        );
        nonEmptyCollection.Value.Should().ContainInConsecutiveOrder(1, 2, 3);
        nonEmptyCollection.Value.First().Should().Be(1);
    }

    [Fact(DisplayName = "An empty collection can be refined")]
    public static void Case5()
    {
        Refined<string[], Empty<string[]>> emptyCollection = Array.Empty<string>();
        emptyCollection.Value.Should().BeEmpty();
        Refined
            .TryRefine<string[], Empty<string[]>>(Array.Empty<string>(), out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A non empty collection cannot be refined")]
    public static void Case6()
    {
        Refined
            .TryRefine<string[], Empty<string[]>>(new[] { "Hello", "World" }, out _)
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName = "A non empty collection cannot be refined and throws")]
    public static void Case7()
    {
        var act = () => (Refined<string[], Empty<string[]>>)new[] { "Hello", "World" };
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("Value must be empty")
            .And.Value.Should()
            .BeOfType<string[]>()
            .Which.Should()
            .BeEquivalentTo("Hello", "World");
    }

    [Fact(DisplayName = "A empty enumerable can be refined with empty")]
    public static void Case8()
    {
        var emptyCollection = Refined.Refine<IEnumerable<int>, Empty<IEnumerable<int>>>(
            Enumerable.Empty<int>()
        );
        emptyCollection.Value.Should().BeEmpty();
    }

    [Fact(DisplayName = "A collection can be refined by size")]
    public static void Case9()
    {
        Refined<string[], Size<string[], Even>> refined = new[] { "Hello", "World" };
        refined.Value.Should().BeEquivalentTo("Hello", "World");
        Refined
            .TryRefine<string[], Size<string[], Even>>(new[] { "Hello", "World" }, out _)
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName = "A collection cannot be refined by size")]
    public static void Case10()
    {
        Refined
            .TryRefine<string[], Size<string[], Even>>(new[] { "Hello", "World", "!" }, out _)
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName = "A collection cannot be refined by size and throws")]
    public static void Case11()
    {
        var act = () => (Refined<string[], Size<string[], Even>>)new[] { "Hello", "World", "!" };
        act.Should()
            .Throw<RefinementFailureException>()
            .WithMessage("The values size failed refinement: Value must be even")
            .And.Value.Should()
            .BeOfType<string[]>()
            .Which.Should()
            .BeEquivalentTo("Hello", "World", "!");
    }
}
