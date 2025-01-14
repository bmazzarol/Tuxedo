using FluentAssertions;

namespace Tuxedo.Tests;

/// <summary>
/// This is a non-empty list
/// </summary>
/// <typeparam name="T">some T</typeparam>
public readonly partial struct NonEmptyList<T>
{
    /// <summary>
    /// The head of the list
    /// </summary>
    public T Head => Value[0];

    /// <summary>
    /// Refinement that ensures the list is non-empty
    /// </summary>
    /// <param name="list">list</param>
    /// <typeparam name="T">some T, required again here for the source generator</typeparam>
    /// <returns>true if the list is non-empty</returns>
    [Refinement("The list must not be empty.")]
    private static bool NonEmpty(List<T> list) => list.Count != 0;
}

public class NonEmptyListTests
{
    [Fact(DisplayName = "A list can be refined to be non-empty.")]
    public static void Case1()
    {
        var refined = (NonEmptyList<int>)new List<int> { 1, 2, 3 };
        refined.Value.Should().BeEquivalentTo([1, 2, 3]);
        refined.Head.Should().Be(1);

        NonEmptyList<int>
            .TryParse([1, 2, 3], out var refined2, out var failureMessage)
            .Should()
            .Be(true);
        refined2.Value.Should().BeEquivalentTo(refined.Value);
        failureMessage.Should().BeNull();
    }

    [Fact(DisplayName = "A empty list cannot be refined as non-empty.")]
    public static void Case2()
    {
        List<string> value = [];
        Assert
            .Throws<ArgumentOutOfRangeException>(() => (NonEmptyList<string>)value)
            .Message.Should()
            .StartWith("The list must not be empty.");
        NonEmptyList<string>.TryParse(value, out _, out _).Should().BeFalse();
    }
}
