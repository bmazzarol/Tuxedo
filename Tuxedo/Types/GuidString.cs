using Tuxedo.Refinements;
using static Tuxedo.IRefinedType<
    Tuxedo.Types.GuidString,
    Tuxedo.Refinements.Formatted<System.Guid>,
    string,
    System.Guid
>;

namespace Tuxedo.Types;

/// <summary>
/// Represents a string that is a valid GUID
/// </summary>
public readonly struct GuidString : IRefinedType<GuidString, Formatted<Guid>, string, Guid>
{
    private GuidString(string value, Guid refinedValue)
    {
        RawValue = value;
        RefinedValue = refinedValue;
    }

    /// <inheritdoc />
    public string RawValue { get; }

    /// <inheritdoc />
    public static implicit operator string(GuidString refinedValue)
    {
        return refinedValue.RawValue;
    }

    /// <inheritdoc />
    public Guid RefinedValue { get; }

    /// <inheritdoc />
    public static implicit operator Guid(GuidString refinedValue)
    {
        return refinedValue.RefinedValue;
    }

    /// <inheritdoc />
    public static implicit operator GuidString(string value)
    {
        return Refine(value);
    }

    /// <inheritdoc />
    public static GuidString CreateUnsafe(string value, Guid refinedValue)
    {
        return new(value, refinedValue);
    }

    /// <inheritdoc />
    public void Deconstruct(out string rawValue, out Guid refinedValue)
    {
        rawValue = RawValue;
        refinedValue = RefinedValue;
    }
}
