using System.Text.RegularExpressions;

namespace Tuxedo;

/// <summary>
/// Enforces that a string value matches a regular expression
/// </summary>
/// <typeparam name="TRegex">the regular expression to match</typeparam>
public readonly struct MatchesRegex<TRegex>
    : IRefinementResult<MatchesRegex<TRegex>, MatchCollection>
    where TRegex : struct, IConstant<TRegex, string>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) => value is string s && TryRefine(s, out _);

    /// <inheritdoc />
    public bool TryRefine<T>(T value, out MatchCollection refinedValue)
    {
        if (value is not string s)
        {
            refinedValue = default!;
            return false;
        }

        refinedValue = Regex.Matches(
            s,
            default(TRegex).Value,
            RegexOptions.None,
            TimeSpan.FromSeconds(30)
        );
        return refinedValue.Count > 0;
    }

    /// <inheritdoc cref="IRefinement{TThis}.BuildFailureMessage{T}" />
    public string BuildFailureMessage<T>(T value) =>
        $"Value must match the regular expression '{default(TRegex).Value}'";
}
