namespace Tuxedo;

internal static class BoolRefinements
{
    [Refinement("The boolean value must be 'True', instead found '{value}'")]
    internal static bool True(bool value) => value;

    [Refinement("The boolean value must be 'False', instead found '{value}'")]
    internal static bool False(bool value) => !value;
}
