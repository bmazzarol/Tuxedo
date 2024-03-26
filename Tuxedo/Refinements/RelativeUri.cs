using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value is a valid relative <see cref="Uri"/>
/// </summary>
public readonly struct RelativeUri : IRefinementResult<RelativeUri, Uri>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => TryRefine(value, out _);

    /// <inheritdoc />
    public bool TryRefine<TIn>(TIn value, [NotNullWhen(true)] out Uri? refinedValue)
    {
        if (value is string s && Uri.TryCreate(s, UriKind.Relative, out var uri))
        {
            refinedValue = uri;
            return true;
        }

        refinedValue = null;
        return false;
    }

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) => "Value must be a valid relative URI";
}
