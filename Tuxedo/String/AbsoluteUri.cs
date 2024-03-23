using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid absolute <see cref="Uri"/>
/// </summary>
public readonly struct AbsoluteUri : IRefinement<AbsoluteUri, string, Uri>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) => Uri.TryCreate(value, UriKind.Absolute, out _);

    /// <inheritdoc />
    public bool TryApplyRefinement(string value, out string refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public bool TryRefine(string value, [NotNullWhen(true)] out Uri? refinedValue)
    {
        if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
        {
            refinedValue = uri;
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage(string value) => "Value must be a valid absolute URI";
}
