﻿//HintName: FalseBool.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace <global namespace>;

/// <summary>
/// A refined bool based on the Test.False refinement predicate
/// </summary>
public readonly partial struct FalseBool : IEquatable<FalseBool>
{
    private readonly bool? _value;
   
    /// <summary>
    /// The underlying bool
    /// </summary>
    public bool Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a FalseBool");

    /// <summary>
    /// Implicit conversion from the FalseBool to a bool
    /// </summary>
    /// <param name="this">the FalseBool</param>
    /// <returns>underlying bool</returns>
    public static implicit operator bool(FalseBool @this)
    {
        return @this.Value;
    }
    
    private FalseBool(bool value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a bool to a FalseBool
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined FalseBool</returns>
    /// <exception cref="InvalidOperationException">if the Test.False refinement fails</exception>
    public static explicit operator FalseBool(bool value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the bool or throws
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined FalseBool</returns>
    /// <exception cref="InvalidOperationException">if the Test.False refinement fails</exception>
    public static FalseBool Parse(bool value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new InvalidOperationException(failureMessage);
    }
    
    /// <summary>
    /// Try and refine the bool against the Test.False refinement
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <param name="refined">refined FalseBool when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        bool value,
        out FalseBool refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.False(value))
        {
            refined = new FalseBool(value);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = $"The boolean value must be 'False', instead found '{value}'";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(FalseBool other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is FalseBool other && Equals(other);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
}