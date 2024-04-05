using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value starts with a specific prefix
/// </summary>
/// <typeparam name="TPrefix">prefix type</typeparam>
public readonly struct StartsWith<TPrefix> : IRefinement<StartsWith<TPrefix>, string>
    where TPrefix : struct, IConstant<TPrefix, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value, [NotNullWhen(false)] out string? failureMessage)
    {
        if (value.StartsWith(default(TPrefix).Value, StringComparison.Ordinal))
        {
            failureMessage = null;
            return true;
        }

        var prefix = default(TPrefix).Value;
        failureMessage =
            $"Value must start with '{prefix}' but started with '{value[..prefix.Length]}'";
        return false;
    }
}
