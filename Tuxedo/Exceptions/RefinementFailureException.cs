using System.Diagnostics.CodeAnalysis;

namespace Tuxedo;

/// <summary>
/// Thrown when an attempt to refine a value fails
/// </summary>
[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors.")]
public sealed class RefinementFailureException : Exception
{
    /// <summary>
    /// Value that could not be refined
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Type of the value that could not be refined
    /// </summary>
    public Type? ValueType => Value?.GetType();

    internal RefinementFailureException(object? value, string message)
        : base(message)
    {
        Value = value;
    }
}
