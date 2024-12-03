using System.Collections;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be non-empty
/// </summary>
public sealed class NonEmpty<T> : Refinement<NonEmpty<T>, T>
{
    /// <inheritdoc />
    protected override bool IsRefined(T value)
    {
        return value switch
        {
            string s => !string.IsNullOrEmpty(s),
            ICollection c => c.Count > 0,
            IEnumerable e => e.GetEnumerator().MoveNext(),
            _ => value is not null,
        };
    }

    /// <inheritdoc />
    protected override string BuildFailureMessage(T value) => "Value must be non-empty";
}
