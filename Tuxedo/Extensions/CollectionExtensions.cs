namespace Tuxedo;

/// <summary>
/// Extension methods for collections refined using <see cref="NonEmpty{TEnumerable, T}"/>
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Enforces that a collection is not empty
    /// </summary>
    /// <param name="value">collection</param>
    /// <typeparam name="T">element type</typeparam>
    /// <returns>refined collection</returns>
    public static Refined<IEnumerable<T>, NonEmpty<IEnumerable<T>, T>> ToNonEmpty<T>(
        this IEnumerable<T> value
    ) => Refined.Refine<IEnumerable<T>, NonEmpty<IEnumerable<T>, T>>(value);

    /// <summary>
    /// Returns the first element of a refined <see cref="NonEmpty{TEnumerable, T}"/> collection
    /// </summary>
    /// <param name="value">refined value</param>
    /// <typeparam name="TEnumerable">collection type</typeparam>
    /// <typeparam name="T">element type</typeparam>
    /// <returns>first element; never throws</returns>
    public static T First<TEnumerable, T>(this Refined<TEnumerable, NonEmpty<TEnumerable, T>> value)
        where TEnumerable : IEnumerable<T> =>
        value.Value switch
        {
            IList<T> collection => collection[0],
            _ => value.Value.First()
        };
}
