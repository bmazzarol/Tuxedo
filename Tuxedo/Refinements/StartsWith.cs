namespace Tuxedo;

/// <summary>
/// Enforces that a string value starts with a specific prefix
/// </summary>
/// <typeparam name="TPrefix">prefix type</typeparam>
public readonly struct StartsWith<TPrefix> : IRefinement<StartsWith<TPrefix>, string>
    where TPrefix : struct, IConstant<TPrefix, string>
{
    /// <inheritdoc />
    public bool CanBeRefined(string value) =>
        value.StartsWith(default(TPrefix).Value, StringComparison.Ordinal);

    /// <inheritdoc />
    public string BuildFailureMessage(string value) =>
        $"Value must start with '{default(TPrefix).Value}'";
}
