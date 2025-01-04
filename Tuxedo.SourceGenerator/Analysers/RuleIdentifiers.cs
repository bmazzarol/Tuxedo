namespace Tuxedo.SourceGenerator.Analysers;

/// <summary>
/// Supported analyser rules
/// </summary>
public static class RuleIdentifiers
{
    /// <summary>
    /// Refined types should not be created via default
    /// </summary>
    public const string DoNotUseDefault = "TUX001";

    /// <summary>
    /// Refined types should not be created via new
    /// </summary>
    public const string DoNotUseNew = "TUX002";

    /// <summary>
    /// Compile type known values that will fail refinement, should not be assigned
    /// </summary>
    public const string InvalidConstAssignment = "TUX003";
}
