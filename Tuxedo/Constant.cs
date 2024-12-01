﻿namespace Tuxedo;

/// <summary>
/// Represents a type level constant
/// </summary>
/// <typeparam name="TThis">the type of the constant</typeparam>
/// <typeparam name="T">the type of the constant</typeparam>
public abstract class Constant<TThis, T>
    where TThis : Constant<TThis, T>, new()
{
    /// <summary>
    /// Singleton instance of the constant
    /// </summary>
    public static TThis Value { get; } = new();

    /// <summary>
    /// Constant value
    /// </summary>
    public abstract T ConstValue { get; }
}