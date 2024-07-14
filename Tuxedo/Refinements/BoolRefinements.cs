namespace Tuxedo;

/// <summary>
/// Boolean refinements
/// </summary>
public static class BoolRefinements
{
    /// <summary>
    /// Enforces that a boolean value is true
    /// </summary>
    [Refinement("The boolean value must be 'True', instead found '{value}'")]
    public static bool True(bool value) => value;

    /// <summary>
    /// Enforces that a boolean value is false
    /// </summary>
    [Refinement("The boolean value must be 'False', instead found '{value}'")]
    public static bool False(bool value) => !value;
}
