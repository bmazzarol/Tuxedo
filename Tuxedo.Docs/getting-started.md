# Getting Started

To use this source generator include it as a source only dependency in your
project.

```xml
<ItemGroup>
    <PackageReference Include="Tuxedo.SourceGenerator" Version="x.x.x">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
</ItemGroup>
```

Now we can build our first refined type.

Let's create a password string that must comply with some simple rules.

It must,

* Be at least 8 characters long
* Contain at least 1 uppercase letter
* Contain at least 1 number
* Contain at least 1 special character

To do this create a simple predicate method, It helps to do this on the
partial struct that will eventually be the refined type.

So in this case that type will be called a `PasswordString`, it looks like this,

[!code-csharp[](../Tuxedo.SourceGenerator.Tests/PasswordStringExample.cs#ExampleRefinement)]

Which can be used like this,

[!code-csharp[Example1](../Tuxedo.SourceGenerator.Tests/PasswordStringExample.cs#ExampleUsage)]
