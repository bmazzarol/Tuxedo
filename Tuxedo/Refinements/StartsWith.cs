namespace Tuxedo;

/// <summary>
/// Enforces that a string value starts with a specific prefix
/// </summary>
/// <typeparam name="TPrefix">prefix type</typeparam>
public readonly struct StartsWith<TPrefix> : IRefinement<StartsWith<TPrefix>>
    where TPrefix : struct, IConstant<TPrefix, string>
{
    /// <inheritdoc />
    public bool CanBeRefined<T>(T value) =>
        value is string s && s.StartsWith(default(TPrefix).Value, StringComparison.Ordinal);

    /// <inheritdoc />
    public string BuildFailureMessage<T>(T value) =>
        $"Value must start with '{default(TPrefix).Value}'";
}
