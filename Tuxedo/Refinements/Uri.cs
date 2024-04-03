using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="System.Uri"/> matching a specific <see cref="UriKind"/>
/// </summary>
public readonly struct Uri<TKind> : IRefinement<Uri<TKind>, string, System.Uri>
    where TKind : struct, IConstant<TKind, UriKind>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine(string value, [NotNullWhen(true)] out System.Uri? refinedValue)
    {
        if (System.Uri.TryCreate(value, default(TKind).Value, out var uri))
        {
            refinedValue = uri;
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) => "Value must be a valid URI";
}

/// <summary>
/// Enforces that a string value is a valid <see cref="System.Uri"/>
/// </summary>
public readonly struct Uri : IRefinement<Uri, string, System.Uri>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine(string value, [NotNullWhen(true)] out System.Uri? refinedValue)
    {
        if (System.Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uri))
        {
            refinedValue = uri;
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) => "Value must be a valid URI";
}

/// <summary>
/// Represents a <see cref="UriKind.Absolute"/> at the type level
/// </summary>
public readonly struct UriKindAbsolute : IConstant<UriKindAbsolute, UriKind>
{
    /// <inheritdoc />
    public UriKind Value => UriKind.Absolute;
}

/// <summary>
/// Represents a <see cref="UriKind.Relative"/> at the type level
/// </summary>
public readonly struct UriKindRelative : IConstant<UriKindRelative, UriKind>
{
    /// <inheritdoc />
    public UriKind Value => UriKind.Relative;
}
