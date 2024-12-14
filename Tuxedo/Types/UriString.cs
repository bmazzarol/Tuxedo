namespace Tuxedo.Types;

/// <summary>
/// Represents a string that is a valid absolute or relative URI
/// </summary>
public readonly struct UriString<TKind>
    : IRefinedType<UriString<TKind>, UriString<TKind>, string, Uri>,
        IRefinement<UriString<TKind>, string, Uri>
    where TKind : IConstant<TKind, UriKind>
{
    private UriString(string rawValue, Uri refinedValue)
    {
        RawValue = rawValue;
        RefinedValue = refinedValue;
    }

    /// <inheritdoc />
    public string RawValue { get; }

    /// <inheritdoc />
    public static implicit operator string(UriString<TKind> refinedValue)
    {
        return refinedValue.RawValue;
    }

    /// <inheritdoc />
    public Uri RefinedValue { get; }

    /// <inheritdoc />
    public static implicit operator Uri(UriString<TKind> refinedValue)
    {
        return refinedValue.RefinedValue;
    }

    /// <inheritdoc />
    public static implicit operator UriString<TKind>(string value)
    {
        return IRefinedType<UriString<TKind>, UriString<TKind>, string, Uri>.Refine(value);
    }

    /// <inheritdoc />
    public static UriString<TKind> CreateUnsafe(string value, Uri refinedValue)
    {
        return new(value, refinedValue);
    }

    /// <inheritdoc />
    public void Deconstruct(out string rawValue, out Uri refinedValue)
    {
        rawValue = RawValue;
        refinedValue = RefinedValue;
    }

    /// <inheritdoc />
    public static UriString<TKind> Value { get; }

    /// <inheritdoc />
    public bool IsRefined(string value, out Uri refinedValue)
    {
        return Uri.TryCreate(value, TKind.Value, out refinedValue!);
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value)
    {
        return $"Value must be a valid Uri, but was '{value}'";
    }
}
