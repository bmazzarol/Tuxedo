﻿//HintName: Case3.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined bool based on the Test.ExampleWithReturnedFailureMessage refinement predicate
/// </summary>
[RefinedType]
public readonly partial struct Case3 : IEquatable<Case3>
{
    private readonly bool? _value;
   
    /// <summary>
    /// The underlying bool
    /// </summary>
    public bool Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a Case3");

    /// <summary>
    /// Implicit conversion from the Case3 to a bool
    /// </summary>
    /// <param name="this">the Case3</param>
    /// <returns>underlying bool</returns>
    public static implicit operator bool(Case3 @this)
    {
        return @this.Value;
    }
    
    private Case3(bool value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a bool to a Case3
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined Case3</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.ExampleWithReturnedFailureMessage refinement fails</exception>
    public static explicit operator Case3(bool value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the bool or throws
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <returns>refined Case3</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.ExampleWithReturnedFailureMessage refinement fails</exception>
    public static Case3 Parse(bool value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the bool against the Test.ExampleWithReturnedFailureMessage refinement
    /// </summary>
    /// <param name="value">raw bool</param>
    /// <param name="refined">refined Case3 when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        bool value,
        out Case3 refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.ExampleWithReturnedFailureMessage(value) is not {} fm)
        {
            refined = new Case3(value);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = fm;
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(Case3 other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Case3 other && Equals(other);
    }
    
    /// <inheritdoc />
    public static bool operator ==(Case3 left, Case3 right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc />
    public static bool operator !=(Case3 left, Case3 right)
    {
        return !(left == right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    /// <inheritdoc />
    public override string? ToString()
    {
        return _value?.ToString();
    }
}