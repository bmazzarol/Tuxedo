namespace Tuxedo;

/// <summary>
/// Provides a type level constant
/// </summary>
/// <typeparam name="TThis">the type of the constant; must be a struct type to enable lookup</typeparam>
/// <typeparam name="T">the type of the constant</typeparam>
public interface IConstant<TThis, out T>
    where TThis : struct, IConstant<TThis, T>
{
    /// <summary>
    /// Constant value
    /// </summary>
    T Value { get; }
}
