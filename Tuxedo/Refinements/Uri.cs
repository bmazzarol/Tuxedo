using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="System.Uri"/> matching a specific <see cref="UriKind"/>
/// </summary>
public readonly struct Uri<TKind> : IRefinement<Uri<TKind>, string, System.Uri>
    where TKind : struct, IConstant<TKind, UriKind>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage) =>
        TryRefine(value, out _, out failureMessage);

    /// <inheritdoc />
    public bool TryRefine(
        string value,
        [NotNullWhen(true)] out System.Uri? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (System.Uri.TryCreate(value, default(TKind).Value, out var uri))
        {
            refinedValue = uri;
            failureMessage = null;
            return true;
        }

        refinedValue = null;
        failureMessage =
            $"Value must be a valid {default(TKind).Value.ToString().ToLowerInvariant()} URI";
        return false;
    }
}

/// <summary>
/// Enforces that a string value is a valid <see cref="System.Uri"/>
/// </summary>
public readonly struct Uri : IRefinement<Uri, string, System.Uri>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage) =>
        TryRefine(value, out _, out failureMessage);

    /// <inheritdoc />
    public bool TryRefine(
        string value,
        [NotNullWhen(true)] out System.Uri? refinedValue,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (System.Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uri))
        {
            refinedValue = uri;
            failureMessage = null;
            return true;
        }

        refinedValue = null;
        failureMessage = "Value must be a valid URI";
        return false;
    }
}

/// <summary>
/// Represents a <see cref="UriKind.Absolute"/> at the type level
/// </summary>
public readonly struct AbsoluteKind : IConstant<AbsoluteKind, UriKind>
{
    /// <inheritdoc />
    public UriKind Value => UriKind.Absolute;
}

/// <summary>
/// Represents a <see cref="UriKind.Relative"/> at the type level
/// </summary>
public readonly struct RelativeKind : IConstant<RelativeKind, UriKind>
{
    /// <inheritdoc />
    public UriKind Value => UriKind.Relative;
}
