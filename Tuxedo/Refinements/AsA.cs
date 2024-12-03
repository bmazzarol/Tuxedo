using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be a valid GUID
/// </summary>
public sealed class AsA<T> : Refinement<AsA<T>, string, T>
    where T : IParsable<T>
{
    /// <inheritdoc />
    protected override bool IsRefined(string value, [NotNullWhen(true)] out T refinedValue) =>
        T.TryParse(value, CultureInfo.InvariantCulture, out refinedValue!);

    /// <inheritdoc />
    protected override string BuildFailureMessage(string value) =>
        $"Value must be a valid {typeof(T).Name}, but was '{value}'";
}
