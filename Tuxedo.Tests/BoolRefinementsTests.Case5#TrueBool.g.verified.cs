﻿//HintName: TrueBool.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace <global namespace>;

/// <summary>
/// A refined bool based on the Test.True refinement predicate
/// </summary>
public readonly partial struct TrueBool : IEquatable<TrueBool>
{
    private readonly bool? _value;
   
    /// <summary>
    /// The underlying bool
    /// </summary>
    public bool Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a TrueBool");

    /// <summary>
    /// Implicit conversion from the TrueBool to a bool
    /// </summary>
    /// <param name="this">the TrueBool</param>
    /// <returns>underlying bool</returns>
    public static implicit operator bool(TrueBool @this)
    {
        return @this.Value;
    }
    
    private TrueBool(bool value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a bool to a TrueBool
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined TrueBool</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.True refinement fails</exception>
    public static explicit operator TrueBool(bool value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the bool or throws
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined TrueBool</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.True refinement fails</exception>
    public static TrueBool Parse(bool value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the bool against the Test.True refinement
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <param name="refined">refined TrueBool when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        bool value,
        out TrueBool refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.True(value))
        {
            refined = new TrueBool(value);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = $"The boolean value must be 'True', instead found '{value}'";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(TrueBool other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is TrueBool other && Equals(other);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
}