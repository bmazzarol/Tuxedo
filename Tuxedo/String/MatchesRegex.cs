using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value matches a regular expression
/// </summary>
/// <typeparam name="TRegex">the regular expression to match</typeparam>
public readonly struct MatchesRegex<TRegex>
    : IRefinement<MatchesRegex<TRegex>, string, MatchCollection>
    where TRegex : struct, IConstant<TRegex, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) =>
        Regex.IsMatch(value, default(TRegex).Value, RegexOptions.None, TimeSpan.FromSeconds(30));

    /// <inheritdoc />
    public bool TryApplyRefinement(string value, [NotNullWhen(true)] out string? refinedValue)
    {
        refinedValue = value;
        return false;
    }

    /// <inheritdoc />
    public bool TryRefine(string value, out MatchCollection refinedValue)
    {
        refinedValue = Regex.Matches(
            value,
            default(TRegex).Value,
            RegexOptions.None,
            TimeSpan.FromSeconds(30)
        );
        return refinedValue.Count > 0;
    }

    /// <inheritdoc cref="IRefinement{TThis,T}.BuildFailureMessage" />
    public string BuildFailureMessage(string value) =>
        $"Value must match the regular expression '{default(TRegex).Value}'";
}
