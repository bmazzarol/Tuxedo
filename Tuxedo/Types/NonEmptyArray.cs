using Tuxedo.Refinements;

namespace Tuxedo.Types;

/// <summary>
/// Represents a non-empty array
/// </summary>
/// <typeparam name="T">type of the elements in the array</typeparam>
public readonly struct NonEmptyArray<T> : IRefinedType<NonEmptyArray<T>, NonEmpty, T[]>
{
    private NonEmptyArray(T[] value)
    {
        Value = value;
    }

    /// <inheritdoc />
    public T[] Value { get; }

    /// <summary>
    /// Head of the array
    /// </summary>
    public T Head => Value[0];

    /// <inheritdoc />
    public static implicit operator T[](NonEmptyArray<T> refinedValue)
    {
        return refinedValue.Value;
    }

    /// <inheritdoc />
    public static implicit operator NonEmptyArray<T>(T[] value)
    {
        return IRefinedType<NonEmptyArray<T>, NonEmpty, T[]>.Refine(value);
    }

    /// <inheritdoc />
    public static NonEmptyArray<T> CreateUnsafe(T[] value)
    {
        return new(value);
    }
}
