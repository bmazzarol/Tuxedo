using System.Collections;
using System.Text.RegularExpressions;
using Tuxedo;

namespace Examples;

/// <summary>
/// An int that must be positive
/// </summary>
public readonly partial struct PositiveInt
{
    [Refinement("Must be positive", Name = nameof(PositiveInt))]
    private bool IsPositive(int value) => value > 0;
}

/// <summary>
/// An array that always has at least 1 value
/// </summary>
/// <typeparam name="T">some T</typeparam>
public readonly partial struct NonEmptyArray<T> : IEnumerable<T>
{
    [Refinement("Must be not empty", Name = "NonEmptyArray")]
    private static bool IsNotEmpty(T[] value) => value.Length > 0;

    public PositiveInt Length => (PositiveInt)Value.Length;

    public T Head => Value[0];

    public T[] Rest => Value[1..];

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)Value).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public readonly partial struct WidgetId
{
    [Refinement(
        "Must be a valid widget ID and match '^W\\d{3}$'. '{value}' does not",
        Name = nameof(WidgetId)
    )]
    private static bool IsValid(string value, out int digit)
    {
        if (ValidWidgetIdRegex().IsMatch(value))
        {
            digit = int.Parse(value[1..]);
            return true;
        }

        digit = 0;
        return false;
    }

    [GeneratedRegex(@"^W\d{3}$")]
    private static partial Regex ValidWidgetIdRegex();
}
