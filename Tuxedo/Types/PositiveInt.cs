using Tuxedo.Refinements;
using static Tuxedo.IRefinedType<Tuxedo.Types.PositiveInt, Tuxedo.Refinements.Positive<int>, int>;

namespace Tuxedo.Types;

/// <summary>
/// Represents a positive integer
/// </summary>
public readonly struct PositiveInt : IRefinedType<PositiveInt, Positive<int>, int>
{
    private PositiveInt(int value)
    {
        Value = value;
    }

    /// <summary>
    /// The underlying value of the refined type
    /// </summary>
    public int Value { get; }

    /// <inheritdoc />
    public static implicit operator int(PositiveInt refinedValue)
    {
        return refinedValue.Value;
    }

    /// <inheritdoc />
    public static implicit operator PositiveInt(int value)
    {
        return Refine(value);
    }

    /// <inheritdoc />
    public static PositiveInt CreateUnsafe(int value)
    {
        return new(value);
    }
}
