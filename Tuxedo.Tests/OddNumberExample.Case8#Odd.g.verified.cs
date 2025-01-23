﻿//HintName: Odd.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined T based on the IsValid refinement predicate
/// </summary>
[RefinedType]
public readonly partial struct Odd<T> : IEquatable<Odd<T>>
	where T : INumberBase<T>
{
    private readonly T? _value;
   
    /// <summary>
    /// The underlying T
    /// </summary>
    public T Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a Odd");

    /// <summary>
    /// Implicit conversion from the Odd&lt;T&gt; to a T
    /// </summary>
    /// <param name="this">the Odd&lt;T&gt;</param>
    /// <returns>underlying T</returns>
    public static implicit operator T(Odd<T> @this)
    {
        return @this.Value;
    }
    
    private Odd(T value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a T to a Odd&lt;T&gt;
    /// </summary>
    /// <param name="value">raw T</param>
    /// <returns>refined Odd&lt;T&gt;</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the IsValid refinement fails</exception>
    public static explicit operator Odd<T>(T value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the T or throws
    /// </summary>
    /// <param name="value">raw T</param>
    /// <returns>refined Odd&lt;T&gt;</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the IsValid refinement fails</exception>
    public static Odd<T> Parse(T value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the T against the IsValid refinement
    /// </summary>
    /// <param name="value">raw T</param>
    /// <param name="refined">refined Odd&lt;T&gt; when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        T value,
        out Odd<T> refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (IsValid(value))
        {
            refined = new Odd<T>(value);
            failureMessage = null;
            return true;
        }
        
        refined = default;
        failureMessage = $"The number must be an odd number, but was '{value}'";
        return false;
    }
    
    /// <inheritdoc />
    public bool Equals(Odd<T> other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Odd<T> other && Equals(other);
    }
    
    /// <inheritdoc />
    public static bool operator ==(Odd<T> left, Odd<T> right)
    {
        return left.Equals(right);
    }
    
    /// <inheritdoc />
    public static bool operator !=(Odd<T> left, Odd<T> right)
    {
        return !(left == right);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    /// <summary>
    /// Returns the string representation of the underlying T
    /// </summary>
    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }
}