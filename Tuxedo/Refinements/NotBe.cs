namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to not be a given refinement
/// </summary>
/// <typeparam name="TRefinement">refinement to not be</typeparam>
/// <typeparam name="T">type of the value</typeparam>
public readonly record struct NotBe<TRefinement, T> : IRefinement<NotBe<TRefinement, T>, T>
    where TRefinement : IRefinement<TRefinement, T>
{
    static NotBe<TRefinement, T> IRefinement<NotBe<TRefinement, T>, T>.Value { get; }

    bool IRefinement<NotBe<TRefinement, T>, T>.IsRefined(T value)
    {
        return !TRefinement.Value.IsRefined(value);
    }

    string IRefinement<NotBe<TRefinement, T>, T>.BuildFailureMessage(T value)
    {
        var message = TRefinement.Value.BuildFailureMessage(value);

        if (message.Contains("must be", StringComparison.Ordinal))
        {
            return message.Replace("must be", "must not be", StringComparison.Ordinal);
        }

        return $"Value must not be: ({message})";
    }
}
