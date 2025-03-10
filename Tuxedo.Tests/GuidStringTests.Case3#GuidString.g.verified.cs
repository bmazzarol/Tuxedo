﻿//HintName: GuidString.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined string based on the Test.Guid refinement predicate which produces an alternative System.Guid value
/// </summary>
[RefinedType]
public readonly partial struct GuidString : IEquatable<GuidString>
{
    private readonly string? _value;
   
    /// <summary>
    /// The underlying string
    /// </summary>
    public string Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a GuidString");

    /// <summary>
    /// Implicit conversion from the GuidString to a string
    /// </summary>
    /// <param name="this">the GuidString</param>
    /// <returns>underlying string</returns>
    public static implicit operator string(GuidString @this)
    {
        return @this.Value;
    }
        
    private readonly System.Guid? _altValue;
   
    /// <summary>
    /// The underlying System.Guid
    /// </summary>
    public System.Guid AltValue => _altValue ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a GuidString");

    /// <summary>
    /// Implicit conversion from the GuidString to a System.Guid
    /// </summary>
    /// <param name="this">the GuidString</param>
    /// <returns>underlying System.Guid</returns>
    public static implicit operator System.Guid(GuidString @this)
    {
        return @this.AltValue;
    }

    private GuidString(string value, System.Guid altValue)
    {
        _value = value;
        _altValue = altValue;
    }

    /// <summary>
    /// Explicit conversion from a string to a GuidString
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined GuidString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.Guid refinement fails</exception>
    public static explicit operator GuidString(string value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the string or throws
    /// </summary>
    /// <param name="value">raw string</param>
    /// <returns>refined GuidString</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.Guid refinement fails</exception>
    public static GuidString Parse(string value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }

    /// <summary>
    /// Try and refine the string against the Test.Guid refinement producing a System.Guid
    /// </summary>
    /// <param name="value">raw string</param>
    /// <param name="refined">refined GuidString when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        string value,
        out GuidString refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.Guid(value, out var altValue))
        {
            refined = new GuidString(value, altValue);
            failureMessage = null;
            return true;
        }
        
        refined = default;
        failureMessage = $"The value must be a valid GUID, but was '{value}'";
        return false;
    }
    
    /// <inheritdoc />
    public bool Equals(GuidString other)
    {
        return Nullable.Equals(_value, other._value) && Nullable.Equals(_altValue, other._altValue);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is GuidString other && Equals(other);
    }
    
    /// <inheritdoc />
    public static bool operator ==(GuidString left, GuidString right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc />
    public static bool operator !=(GuidString left, GuidString right)
    {
        return !(left == right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value, _altValue);
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
    
    /// <summary>
    /// Standard deconstruction to the underlying values
    /// </summary>
    /// <param name="value">raw string</param>
    /// <param name="altValue">The alternative System.Guid produced when the refinement predicate is satisfied</param>
    public void Deconstruct(out string value, out System.Guid altValue)
    {
         value = Value;
         altValue = AltValue;
    }
}