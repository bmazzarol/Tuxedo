# Tuxedo

![Tuxedo](images/jacket-icon.png)

**Types that already know they are valid.**

A source generator that turns a predicate into a refined type, so a value of that type has already passed the check.

```shell
dotnet add package tuxedo.sourcegenerator
```

## Why?

A guard clause proves something about a value for the rest of a method, but the
proof stops at the method boundary. Every nested call has to check again, and
the same condition ends up repeated through the program as a precaution.

Tuxedo turns the predicate into a type. The generator writes a new type whose
constructor runs the predicate, so a value of that type has already passed the
check.

```c#
[Refinement("The float must be positive, but was '{value}'")]
public static bool Positive(float value) => value > 0;

public static float MyCustomDivide(float dividend, PositiveFloat divisor)
{
    return dividend / divisor;
}
```

`PositiveFloat` cannot be constructed with zero, so `MyCustomDivide` does not
need a guard. The check happens at the point the value enters the program.

## What it does not do

* It is not a runtime validation library; the generated types are ordinary C#
  types with a validating constructor
* It does not track values across I/O. A value read from a file or a network
  request has to be refined again when it is read
* It does not add nullable or optional semantics; a refined type is a separate
  type

## See also

* [Getting Started](getting-started.md) - install the generator and write a
  predicate
* [Alternative Values](articles/alternative-values.md) - types that accept more
  than one shape
* [Dependent Types](articles/dependent-types.md) - predicates that depend on
  other refined types
* [Singleton Types](articles/singleton-types.md) - types with one valid value
