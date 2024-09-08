namespace Tuxedo;

/// <summary>
/// Marks a method as a refinement method
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class RefinementAttribute : Attribute
{
    /// <summary>
    /// The message to display when the refinement fails.
    /// The `value` parameter is available for string interpolation.
    /// </summary>
    public string FailureMessage { get; }

    /// <summary>
    /// Indicates whether the refinement is public
    /// </summary>
    public bool IsPublic { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefinementAttribute"/> class.
    /// </summary>
    public RefinementAttribute(string failureMessage, bool isPublic = true)
    {
        FailureMessage = failureMessage;
        IsPublic = isPublic;
    }
}
