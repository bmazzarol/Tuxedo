![Tuxedo](https://raw.githubusercontent.com/bmazzarol/Tuxedo/main/jacket-small-icon.png)

# Tuxedo

**Types that already know they are valid.**

A source generator that turns a predicate into a refined type, so a value of that type has already passed the check.

[![Nuget](https://img.shields.io/nuget/v/Tuxedo.SourceGenerator)](https://www.nuget.org/packages/Tuxedo.SourceGenerator/)

```shell
dotnet add package Tuxedo.SourceGenerator
```

[Getting started](https://bmazzarol.github.io/Tuxedo/getting-started.html) - [Full documentation](https://bmazzarol.github.io/Tuxedo/)

## Why?

A predicate is usually written as a guard: check the value, then pass it on. The
check has to be repeated everywhere the value travels. Tuxedo turns the predicate
into a type, so the check happens once, at the boundary.

## First example

A refined type is a partial struct with a predicate attached to it. The
generator reads the predicate and writes the type.

```csharp
public readonly partial struct PasswordString
{
    [Tuxedo.Refinement(
        "The string must be at least 8 characters long, and contain an uppercase letter, a number and a special character. You provided '{value}'.",
        Name = nameof(PasswordString)
    )]
    private static bool IsValidPassword(string value)
    {
        return value.Length >= 8
            && value.Any(char.IsUpper)
            && value.Any(char.IsDigit)
            && value.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));
    }
}
```

Usage is the same whatever the rule is:

```csharp
if (PasswordString.TryParse("12Da3%sd", out var password, out string failure))
{
    string value = password.Value;
}

// or fail fast with an explicit conversion, which throws when the refinement fails
var password = (PasswordString)"12Da3%sd";
```

## Predicates that produce a value

A refinement can also return a converted value next to the check, which is useful
when the refined type holds more than the raw input.

```csharp
public readonly partial struct GuidString
{
    public byte[] Bytes => AltValue.ToByteArray();

    [Tuxedo.Refinement("The value must be a valid GUID, but was '{value}'")]
    private static bool Guid(string value, out Guid guid) => Guid.TryParse(value, out guid);
}
```

`GuidString` converts implicitly to both `string` and `Guid`, and the guid is
produced in the same pass as the validation.

## What it does not do

- It is not a runtime validation library. The generated types are ordinary C#
  types with a validating constructor
- It does not track values across I/O. A value read from a file or a request has
  to be refined again when it is read
- It is not built for `System.Text.Json` or other serialisation layers
- Compile time checking is limited. Constant assignments to `Parse` and
  `TryParse` are evaluated at compile time; everything else is checked at runtime

## See also

- [Getting started](https://bmazzarol.github.io/Tuxedo/getting-started.html)
- [Alternative values](https://bmazzarol.github.io/Tuxedo/articles/alternative-values.html)
- [Dependent types](https://bmazzarol.github.io/Tuxedo/articles/dependent-types.html)
- [Singleton types](https://bmazzarol.github.io/Tuxedo/articles/singleton-types.html)
