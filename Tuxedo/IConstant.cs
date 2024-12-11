namespace Tuxedo;

/// <summary>
/// Represents a type level constant
/// </summary>
/// <typeparam name="TThis">the type of the constant</typeparam>
/// <typeparam name="T">the type of the constant</typeparam>
public interface IConstant<TThis, out T>
    where TThis : IConstant<TThis, T>
{
    /// <summary>
    /// Singleton instance of the constant
    /// </summary>
    public static abstract T Value { get; }
}
