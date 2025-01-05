﻿//HintName: DateOnlyString.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined string based on the Test.DateOnly refinement predicate which produces an alternative DateOnly value
/// </summary>
[RefinedType]
public readonly partial struct DateOnlyString : IEquatable<DateOnlyString>
{
    private readonly string? _value;
   
    /// <summary>
    /// The underlying string
    /// </summary>
    public string Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a DateOnlyString");

    /// <summary>
    /// Implicit conversion from the DateOnlyString to a string
    /// </summary>
    /// <param name="this">the DateOnlyString</param>
    /// <returns>underlying string</returns>
    public static implicit operator string(DateOnlyString @this)
    {
        return @this.Value;
    }
        
    private readonly DateOnly? _altValue;
   
    /// <summary>
    /// The underlying DateOnly
    /// </summary>
    public DateOnly AltValue => _altValue ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a DateOnlyString");

    /// <summary>
    /// Implicit conversion from the DateOnlyString to a DateOnly
    /// </summary>
    /// <param name="this">the DateOnlyString</param>
    /// <returns>underlying DateOnly</returns>
    public static implicit operator DateOnly(DateOnlyString @this)
    {
        return @this.AltValue;
    }

    private DateOnlyString(string value, DateOnly altValue)
    {
        _value = value;
        _altValue = altValue;
    }

    /// <summary>
    /// Explicit conversion from a string to a DateOnlyString
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined DateOnlyString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.DateOnly refinement fails</exception>
    public static explicit operator DateOnlyString(string value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the string or throws
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined DateOnlyString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.DateOnly refinement fails</exception>
    public static DateOnlyString Parse(string value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }

    /// <summary>
    /// Try and refine the string against the Test.DateOnly refinement producing a DateOnly
    /// </summary>
    /// <param name="value">raw string</param>
    /// <param name="refined">refined DateOnlyString when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        string value,
        out DateOnlyString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.DateOnly(value, out var altValue))
        {
            refined = new DateOnlyString(value, altValue);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = $"The value must be a valid date, but was '{value}'";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(DateOnlyString other)
    {
        return Nullable.Equals(_value, other._value) && Nullable.Equals(_altValue, other._altValue);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is DateOnlyString other && Equals(other);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value, _altValue);
    }
    
    /// <summary>
    /// Standard deconstruction to the underlying values
    /// </summary>
    /// <param name="value">raw string</param>
    /// <param name="altValue">alternative DateOnly</param>
    public void Deconstruct(out string value, out DateOnly altValue)
    {
         value = Value;
         altValue = AltValue;
    }
}