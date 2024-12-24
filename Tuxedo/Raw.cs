namespace Tuxedo;

/// <summary>
/// Represents an unrefined raw value
/// </summary>
/// <typeparam name="T">some unrefined type</typeparam>
public static partial class Raw<T>
{
    /// <summary>
    /// Alternate result value produced from refinement of the raw value
    /// </summary>
    /// <typeparam name="TAlternate">alternative type</typeparam>
    public static partial class Produces<TAlternate>;
}
