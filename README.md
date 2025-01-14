<!-- markdownlint-disable MD033 MD041 -->
<div align="center">

<img src="jacket-icon.png" alt="Tuxedo" width="150px"/>

# Tuxedo

[:running: **_Getting Started_**](https://bmazzarol.github.io/Tuxedo/articles/getting-started.html)
|
[:books: **_Documentation_**](https://bmazzarol.github.io/Tuxedo)

[![Nuget](https://img.shields.io/nuget/v/tuxedo.sourcegenerator)](https://www.nuget.org/packages/tuxedo.sourcegenerator/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Tuxedo&metric=coverage)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Tuxedo)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=bmazzarol_Tuxedo&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=bmazzarol_Tuxedo)
[![CD Build](https://github.com/bmazzarol/tuxedo/actions/workflows/cd-build.yml/badge.svg)](https://github.com/bmazzarol/tuxedo/actions/workflows/cd-build.yml)
[![Check Markdown](https://github.com/bmazzarol/tuxedo/actions/workflows/check-markdown.yml/badge.svg)](https://github.com/bmazzarol/tuxedo/actions/workflows/check-markdown.yml)

Refined types for the suave :cocktail: .NET developer

</div>
<!-- markdownlint-enable MD033 MD041 -->

## Why?

More precise types can help to reduce the number of bugs in your code. Tuxedo
provides a source generator that can turn simple conditions (predicates)
into refined types. There is no other runtime dependency, so it can be used in
libraries without issue.

This provides advantages over the 'Guard' based approaches because once the
check on the type has been done, it carries with it proof of refinement,
eliminating the need to ever check it again.

This allows for opt in fail fast behaviour, while eliminating shotgun parsing
where the same checks are repeated throughout the program "Just in case".

For example, take a simple method that divides 2 floats,

```c#
public static float MyCustomDivide(float dividend, float divisor)
{
    // some check that throws early if the divisor is zero
    // this could also be some fancy "Guard" library it has the same effect
    ArgumentOutOfRangeException.ThrowIfZero(divisor);
    
    // now we are safe and can do the work
    return dividend / divisor;    
}
```

This is fine, as long as the method does not pass its parameters on to another
method. Those nested methods would then need to do the same checks again to
ensure that the float is indeed not zero.

Instead, lets use Tuxedo to refine the float type so that it will
always be greater than zero.

```c#
// this is the predicate that is used to refine the float
// it will produce a new type called a PositiveFloat
// only floats that meet the predicate can ever be one
[Refinement("The float must be positive, but was '{value}'")]
public static bool Positive(float value) => value > 0;

public static float MyCustomDivide(float dividend, PositiveFloat divisor)
{
    return dividend / divisor; // will never fail at this point
}
```

A `PositiveFloat` is a refined version of a float, it must be positive.

That is what a refined type is, a type paired with a predicate that it
passes. Tuxedo makes creating these types simple.

Let's create the canonical example of a non-empty list.

```c#

/// <summary>
/// This is a non-empty list
/// </summary>
/// <typeparam name="T">some T</typeparam>
public readonly partial struct NonEmptyList<T>
{
    /// <summary>
    /// The head of the list
    /// </summary>
    public T Head => Value[0];

    /// <summary>
    /// Refinement that ensures the list is non-empty
    /// </summary>
    /// <param name="list">list</param>
    /// <typeparam name="T">some T, required again here for the source generator</typeparam>
    /// <returns>true if the list is non-empty</returns>
    [Refinement("The list must not be empty.")]
    private static bool NonEmpty(List<T> list) => list.Count != 0;
}
```

This new type will convert to its underlying List of T type and has the
convenience `Head` method added to it.

Using it looks like this,

```c#
// some list we want to refine
List<string> list = new List<string>{ "Some", "Great", "Values" };

// now we can try and convert that list
if(NonEmptyList<string>.TryParse(
    list, 
    out NonEmptyList<string> nel, 
    out string failureMessage))
{
    // we have a safe non-empty list in here
    // which we can get the head of
    var head = nel.Head;
    // or assign to a list variable
    List<string> lst = nel;
    // or enumerate via the standard Value property
    foreach(var e in nel.Value)
    {
        ...
    }
}
// or if we want we can just fail fast and do an explicit conversion which 
// thows if the refinement fails
var nel = (NonEmptyList<string>) list;
```

All refined types are structs and will not allocate, making them cost-effective.

They can also be used to parse, not just validate.

A very common use case is running a predicate which also results in some
alternative value being created via the process.

Take a string that should also be a valid Guid. It would be good to produce
a valid guid as a result of the refinement process.

This can be done with Tuxedo,

```c#
/// <summary>
/// Represents a string that is also a valid Guid
/// </summary>
public readonly partial struct GuidString
{
    // custom fields and methods can be added to the refined type
    public byte[] Bytes => AltValue.ToByteArray();

    public bool IsEmpty => AltValue == System.Guid.Empty;

    [Refinement("The value must be a valid GUID, but was '{value}'")]
    private static bool Guid(string value, out Guid guid) => 
        // the standard TryParse on guid returns true if it's a valid guid
        // and also produces a guid as a result, which gets captured as 
        // part of constructing the GuidString
        System.Guid.TryParse(value, out guid);
}
```

A `GuidString` is now a valid string and guid.

It will implicitly convert to both, and will use TryParse on guid internally
to determine if the string is a valid guid, so in one pass also produces the
guid.

```c#
// convert the string to a GuidString
var refined = (GuidString)"6192C5ED-505C-4558-B87C-CA6E7D612B31";
// now we have a valid Guid
refined.Value.Should().Be("6192C5ED-505C-4558-B87C-CA6E7D612B31"); // string
refined.AltValue.Should().Be(new Guid(
    "6192C5ED-505C-4558-B87C-CA6E7D612B31")); // guid
// and can also use custom methods defined on GuidString
refined.Bytes.Should().NotBeEmpty();
refined.IsEmpty.Should().BeFalse();
```

If the alternative value is the same as the raw value, then only one
implicit conversion method will be generated for the alternative value, not the
raw value.

## Are these Refinement types?

Sort of, at this stage there is limited compile time checks performed.

For constant value assignments to `Parse` and `TryParse` methods, the
refinement is attempted at compile time. There are many edge cases and for
the most part any time this analyser runs, it's a bonus.

## When should I not use this?

Refined types are not designed to integrate with System.Text.Json or any
other serialization layer. That is not to say that they could not be made
to, but at this stage it's a non-requirement.

Just keep the refined types inside the domain boundary of your application
and use the simpler raw types on the edges.

For more details/information keep reading the docs or have a look at the test
projects or create an issue.
