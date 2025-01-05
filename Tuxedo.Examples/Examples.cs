using Tuxedo;

namespace Examples;

/// <summary>
/// An int that must be positive
/// </summary>
public readonly partial struct PositiveInt
{
    [Refinement("Must be positive", Name = nameof(PositiveInt))]
    private static bool IsPositive(int value) => value > 0;
}

/// <summary>
/// An array that always has at least 1 value
/// </summary>
/// <typeparam name="T">some T</typeparam>
public readonly partial struct NonEmptyArray<T>
{
    [Refinement("Must be not empty", Name = "NonEmptyArray")]
    private static bool IsNotEmpty<T>(T[] value) => value.Length > 0;

    public PositiveInt Length => (PositiveInt)Value.Length;

    public T Head => Value[0];

    public T[] Rest => Value[1..];
}
