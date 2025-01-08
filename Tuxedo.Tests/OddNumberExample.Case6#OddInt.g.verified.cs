﻿//HintName: OddInt.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined int based on the Test.Odd refinement predicate
/// </summary>
[RefinedType]
public readonly partial struct OddInt : IEquatable<OddInt>, IFormattable
{
    private readonly int? _value;
   
    /// <summary>
    /// The underlying int
    /// </summary>
    public int Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a OddInt");

    /// <summary>
    /// Implicit conversion from the OddInt to a int
    /// </summary>
    /// <param name="this">the OddInt</param>
    /// <returns>underlying int</returns>
    public static implicit operator int(OddInt @this)
    {
        return @this.Value;
    }
    
    private OddInt(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a int to a OddInt
    /// </summary>
    /// <param name="value">raw int</param>
    /// <returns>refined OddInt</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.Odd refinement fails</exception>
    public static explicit operator OddInt(int value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the int or throws
    /// </summary>
    /// <param name="value">raw int</param>
    /// <returns>refined OddInt</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.Odd refinement fails</exception>
    public static OddInt Parse(int value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the int against the Test.Odd refinement
    /// </summary>
    /// <param name="value">raw int</param>
    /// <param name="refined">refined OddInt when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        int value,
        out OddInt refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.Odd(value))
        {
            refined = new OddInt(value);
            failureMessage = null;
            return true;
        }
        
        refined = default;
        failureMessage = $"The number must be an odd number, but was '{value}'";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(OddInt other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is OddInt other && Equals(other);
    }
    
    /// <inheritdoc />
    public static bool operator ==(OddInt left, OddInt right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc />
    public static bool operator !=(OddInt left, OddInt right)
    {
        return !(left == right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    /// <summary>
    /// Returns the string representation of the underlying int
    /// </summary>
    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }
    
    /// <summary>
    /// Returns the string representation of the underlying int
    /// </summary>
    public string ToString(IFormatProvider? provider)
    {
        return ((IConvertible)Value).ToString(provider) ?? string.Empty;
    }
    
    /// <summary>
    /// Returns the string representation of the underlying int
    /// </summary>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return ((IFormattable)Value).ToString(format, formatProvider) ?? string.Empty;
    }
}