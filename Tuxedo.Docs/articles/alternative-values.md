# Alternative Values

Sometimes the refinement process for a type, naturally produces an alternative
value.

A good example of this is types that implement `IParsable`.

For example take `DateOnly`,

[!code-csharp[Example1](../../Tuxedo.SourceGenerator.Tests/DateOnlyExample.cs#DateOnlyExample)]

This can be used like this,

[!code-csharp[Example1](../../Tuxedo.SourceGenerator.Tests/DateOnlyExample.cs#DateOnlyStringUsage)]

This is optimally efficient as it produces the alternative value as a result
of the refinement process, as opposed to ensuring that the raw value could be
refined (validating it first) and then producing the alternative value
(parsing it).
