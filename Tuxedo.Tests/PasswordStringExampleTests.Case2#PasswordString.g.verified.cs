﻿//HintName: PasswordString.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined string based on the Test.IsValidPassword refinement predicate
/// </summary>
[RefinedType]
public readonly partial struct PasswordString : IEquatable<PasswordString>
{
    private readonly string? _value;
   
    /// <summary>
    /// The underlying string
    /// </summary>
    public string Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a PasswordString");

    /// <summary>
    /// Implicit conversion from the PasswordString to a string
    /// </summary>
    /// <param name="this">the PasswordString</param>
    /// <returns>underlying string</returns>
    public static implicit operator string(PasswordString @this)
    {
        return @this.Value;
    }
    
    private PasswordString(string value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a string to a PasswordString
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined PasswordString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.IsValidPassword refinement fails</exception>
    public static explicit operator PasswordString(string value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the string or throws
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined PasswordString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.IsValidPassword refinement fails</exception>
    public static PasswordString Parse(string value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the string against the Test.IsValidPassword refinement
    /// </summary>
    /// <param name="value">raw string</param>
    /// <param name="refined">refined PasswordString when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        string value,
        out PasswordString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.IsValidPassword(value))
        {
            refined = new PasswordString(value);
            failureMessage = null;
            return true;
        }
        
        refined = default;
        failureMessage = $"The string must be at least 8 characters long, contain at most 1 uppercase letter, number and special character. You provided '{value}'.";
        return false;
    }
    
    /// <inheritdoc />
    public bool Equals(PasswordString other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is PasswordString other && Equals(other);
    }
    
    /// <inheritdoc />
    public static bool operator ==(PasswordString left, PasswordString right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc />
    public static bool operator !=(PasswordString left, PasswordString right)
    {
        return !(left == right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    /// <summary>
    /// Returns the string representation of the underlying string
    /// </summary>
    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }
    
    /// <summary>
    /// Returns the string representation of the underlying string
    /// </summary>
    public string ToString(IFormatProvider? provider)
    {
        return ((IConvertible)Value).ToString(provider) ?? string.Empty;
    }
}