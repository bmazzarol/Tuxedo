# Less Than

The [less than refinement](xref:Tuxedo.LessThan`1) enforces that an
[any numeric type](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types)
is less than the provided comparison value.

The natural numbers up to 100 have been provided as
[constants for convenience](xref:Tuxedo.NaturalNumber).

For example,

[!code-csharp[](../../../../Tuxedo.Tests/Numeric/LessThanTests.cs#LessThanInt)]
