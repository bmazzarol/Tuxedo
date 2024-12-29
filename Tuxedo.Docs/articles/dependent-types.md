# Dependent Types

Tuples can be used to create limited dependent types.

For example, we can create a range type that ensures both values are correct
in terms of each other,

[!code-csharp[Example1](../../Tuxedo.Tests/SafeRangeExample.cs#DependentTypeExample)]

Which can be used like so,

[!code-csharp[Example1](../../Tuxedo.Tests/SafeRangeExample.cs#DependentTypeUsage)]
