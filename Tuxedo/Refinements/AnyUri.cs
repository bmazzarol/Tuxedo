using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid <see cref="Uri"/>
/// </summary>
public readonly struct AnyUri : IRefinementResult<AnyUri, Uri>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine<T>(T value, [NotNullWhen(true)] out Uri? refinedValue)
    {
        if (value is string s && Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out var uri))
        {
            refinedValue = uri;
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be a valid URI";
}
