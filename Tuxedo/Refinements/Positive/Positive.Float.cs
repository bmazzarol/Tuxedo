using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Ensures that an numeric value is positive
/// </summary>
public readonly partial struct Positive : IRefinement<Positive, float>
{
    bool IRefinement<Positive, float>.CanBeRefined(
        float value,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (value > 0)
        {
            failureMessage = null;
            return true;
        }

        failureMessage = $"Value must be positive, but found {value}";
        return false;
    }
}
