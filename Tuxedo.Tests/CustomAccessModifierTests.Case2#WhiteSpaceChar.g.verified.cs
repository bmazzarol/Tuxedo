﻿//HintName: WhiteSpaceChar.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using Tuxedo;

namespace <global namespace>;

/// <summary>
/// A refined char based on the Test.WhiteSpace refinement predicate
/// </summary>
[RefinedType]
internal readonly partial struct WhiteSpaceChar : IEquatable<WhiteSpaceChar>
{
    private readonly char? _value;
   
    /// <summary>
    /// The underlying char
    /// </summary>
    public char Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a WhiteSpaceChar");

    /// <summary>
    /// Implicit conversion from the WhiteSpaceChar to a char
    /// </summary>
    /// <param name="this">the WhiteSpaceChar</param>
    /// <returns>underlying char</returns>
    public static implicit operator char(WhiteSpaceChar @this)
    {
        return @this.Value;
    }
    
    private WhiteSpaceChar(char value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a char to a WhiteSpaceChar
    /// </summary>
    /// <param name="value">raw char</param>
    /// <returns>refined WhiteSpaceChar</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.WhiteSpace refinement fails</exception>
    public static explicit operator WhiteSpaceChar(char value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the char or throws
    /// </summary>
    /// <param name="value">raw char</param>
    /// <returns>refined WhiteSpaceChar</returns>
    /// <exception cref="ArgumentOutOfRangeException">if the Test.WhiteSpace refinement fails</exception>
    public static WhiteSpaceChar Parse(char value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new ArgumentOutOfRangeException(nameof(value), value, failureMessage);
    }
    
    /// <summary>
    /// Try and refine the char against the Test.WhiteSpace refinement
    /// </summary>
    /// <param name="value">raw char</param>
    /// <param name="refined">refined WhiteSpaceChar when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        char value,
        out WhiteSpaceChar refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.WhiteSpace(value))
        {
            refined = new WhiteSpaceChar(value);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = $"`{value}` is not a whitespace character";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(WhiteSpaceChar other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is WhiteSpaceChar other && Equals(other);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
}