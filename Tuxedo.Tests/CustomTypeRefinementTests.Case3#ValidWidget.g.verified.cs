﻿//HintName: ValidWidget.g.cs
// <auto-generated/>
#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;

namespace <global namespace>;

/// <summary>
/// A refined Widget based on the Test.Predicate refinement predicate
/// </summary>
public readonly partial struct ValidWidget : IEquatable<ValidWidget>
{
    private readonly Widget? _value;
   
    /// <summary>
    /// The underlying Widget
    /// </summary>
    public Widget Value => _value ?? throw new InvalidOperationException("Do not use the default value, please use the Parse and TryParse methods to construct a ValidWidget");

    /// <summary>
    /// Implicit conversion from the ValidWidget to a Widget
    /// </summary>
    /// <param name="this">the ValidWidget</param>
    /// <returns>underlying Widget</returns>
    public static implicit operator Widget(ValidWidget @this)
    {
        return @this.Value;
    }
    
    private ValidWidget(Widget value)
    {
        _value = value;
    }

    /// <summary>
    /// Explicit conversion from a Widget to a ValidWidget
    /// </summary>
    /// <param name="value">raw Widget</param>
    /// <returns>refined ValidWidget</returns>
    /// <exception cref="InvalidOperationException">if the Test.Predicate refinement fails</exception>
    public static explicit operator ValidWidget(Widget value)
    {
        return Parse(value);
    }
    
    /// <summary>
    /// Refines the Widget or throws
    /// </summary>
    /// <param name="value">raw Widget</param>
    /// <returns>refined ValidWidget</returns>
    /// <exception cref="InvalidOperationException">if the Test.Predicate refinement fails</exception>
    public static ValidWidget Parse(Widget value)
    {
        return TryParse(value, out var result, out var failureMessage) ? result : throw new InvalidOperationException(failureMessage);
    }
    
    /// <summary>
    /// Try and refine the Widget against the Test.Predicate refinement
    /// </summary>
    /// <param name="value">raw Widget</param>
    /// <param name="refined">refined ValidWidget when true</param>
    /// <param name="failureMessage">error message when false</param>
    /// <returns>true if refined, false otherwise</returns>
    public static bool TryParse(
        Widget value,
        out ValidWidget refined,
        [NotNullWhen(false)] out string? failureMessage
    )
    {
        if (Test.Predicate(value))
        {
            refined = new ValidWidget(value);
            failureMessage = null;
            return true;
        }
        
        refined = default!;
        failureMessage = $"The widget must have a valid Id and Name";
        return false;
    }
    
    // <inheritdoc />
    public bool Equals(ValidWidget other)
    {
        return Nullable.Equals(_value, other._value);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ValidWidget other && Equals(other);
    }
    
    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
}