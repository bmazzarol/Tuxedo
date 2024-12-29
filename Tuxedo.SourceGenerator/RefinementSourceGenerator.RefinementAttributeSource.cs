namespace Tuxedo.SourceGenerator;

public sealed partial class RefinementSourceGenerator
{
    private const string RefinementAttributeSource = """
        // <auto-generated/>
        #nullable enable

        /// <summary>
        /// Marks a method as a refinement to a type
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        internal sealed class RefinementAttribute : Attribute
        {
            /// <summary>
            /// The message to display when the refinement fails.
            /// The `value` parameter is available for string interpolation.
            /// </summary>
            public string FailureMessage { get; }

            /// <summary>
            /// Indicates whether the refined type is internal, default is public
            /// </summary>
            public bool IsInternal { get; set; }
            
            /// <summary>
            /// Optional name of the refined type.
            /// Defaults to the refinement method name + the raw type name.
            /// </summary>
            public string? Name { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="RefinementAttribute"/> class.
            /// </summary>
            public RefinementAttribute(string failureMessage)
            {
                FailureMessage = failureMessage;
            }
        }    
        """;
}
