namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be equal to a constant
/// </summary>
/// <typeparam name="T">type of the value</typeparam>
/// <typeparam name="TOther">type of the constant</typeparam>
public sealed class Equal<T, TOther> : Refinement<Equal<T, TOther>, T>
    where T : IComparable<T>
    where TOther : Constant<TOther, T>, new()
{
    /// <inheritdoc />
    protected override bool IsRefined(T value) => value.Equals(Constant<TOther, T>.Inst.Value);

    /// <inheritdoc />
    protected override string BuildFailureMessage(T value) =>
        $"Value must be equal to '{Constant<TOther, T>.Inst.Value}', but was '{value}'";
}
