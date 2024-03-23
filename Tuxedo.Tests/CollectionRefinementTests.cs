namespace Tuxedo.Tests;

public static class CollectionRefinementTests
{
    [Fact(DisplayName = "A non empty collection can be refined")]
    public static void Case1()
    {
        var nonEmptyCollection = new[] { "Hello", "World" }.ToNonEmpty();
        ((string[])nonEmptyCollection).Should().BeEquivalentTo("Hello", "World");
        Refined
            .TryRefine<string[], NonEmpty<string[], string>>(new[] { "Hello", "World" }, out _)
            .Should()
            .BeTrue();
        nonEmptyCollection.First().Should().Be("Hello");
    }

    [Fact(DisplayName = "An empty collection cannot be refined")]
    public static void Case2()
    {
        Refined
            .TryRefine<string[], NonEmpty<string[], string>>(Array.Empty<string>(), out _)
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName = "An empty collection cannot be refined and throws")]
    public static void Case3()
    {
        var act = () => (Refined<string[], NonEmpty<string[], string>>)Array.Empty<string>();
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
        var nonEmptyCollection = Refined.Refine<IEnumerable<int>, NonEmpty<IEnumerable<int>, int>>(
            Enumerable.Range(1, 3)
        );
        nonEmptyCollection.Value.Should().ContainInConsecutiveOrder(1, 2, 3);
        nonEmptyCollection.First().Should().Be(1);
    }
}
