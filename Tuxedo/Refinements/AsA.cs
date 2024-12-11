﻿using System.Globalization;

namespace Tuxedo.Refinements;

/// <summary>
/// Refinement that enforces a value to be a valid T
/// </summary>
public readonly struct AsA<T> : IRefinement<AsA<T>, string, T>
    where T : IParsable<T>
{
    static AsA<T> IRefinement<AsA<T>, string, T>.Value { get; }

    bool IRefinement<AsA<T>, string, T>.IsRefined(string value, out T refinedValue)
    {
        return T.TryParse(value, CultureInfo.InvariantCulture, out refinedValue!);
    }

    string IRefinement<AsA<T>, string, T>.BuildFailureMessage(string value)
    {
        return $"Value must be a valid {typeof(T).Name}, but was '{value}'";
    }
}
