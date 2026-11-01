# Getting Started

Tuxedo is a source generator, so it is referenced as a build-time only
dependency.

```xml
<ItemGroup>
    <PackageReference Include="Tuxedo.SourceGenerator" Version="x.x.x">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
</ItemGroup>
```

## A refined type

A refined type is a partial struct with a predicate attached to it. The
generator reads the predicate and writes the type.

Say a password has to be at least eight characters long, and contain an
uppercase letter, a number and a special character.

[!code-csharp[](../Tuxedo.SourceGenerator.Tests/PasswordStringExample.cs#ExampleRefinement)]

The generated `PasswordString` carries the rule, and the way to use it is the
same whatever the rule is,

[!code-csharp[Example1](../Tuxedo.SourceGenerator.Tests/PasswordStringExample.cs#ExampleUsage)]

`TryParse` returns `false` and fills the failure message when the value does
not satisfy the predicate. `Parse` throws `ArgumentOutOfRangeException` with
that same message instead.

## Where the rules come from

The generator only turns predicates marked with `Tuxedo.Refinement` into types.
Everything else in the partial struct is left alone.

## See also

* [Refinement Rules](rules/toc.yml) - what the generator requires of a predicate
* [Examples](articles/toc.yml) - refined types for strings, numbers, dates and
  collections
