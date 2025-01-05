using FluentAssertions;
using Tuxedo.Tests.Extensions;

namespace Tuxedo.Tests;

#region ExampleRefinement

/// <summary>
/// A valid password string
/// </summary>
public readonly partial struct PasswordString
{
    // this is the definition of what makes a password string valid.
    // the name of the method combined with the type it operates on make up the name of the refined type
    // in this case PasswordString
    [Tuxedo.Refinement(
        "The string must be at least 8 characters long, contain at most 1 uppercase letter, number and special character. You provided '{value}'.",
        Name = nameof(PasswordString)
    )]
    private static bool IsValidPassword(string value)
    {
        if (value.Length < 8)
        {
            return false;
        }

        var hasUppercaseLetter = false;
        var hasNumber = false;
        var hasSpecialCharacter = false;
        foreach (var @char in value)
        {
            hasUppercaseLetter |= char.IsUpper(@char);
            hasNumber |= char.IsDigit(@char);
            hasSpecialCharacter |= !char.IsLetterOrDigit(@char) && !char.IsWhiteSpace(@char);
        }

        return hasUppercaseLetter && hasNumber && hasSpecialCharacter;
    }
}

#endregion

public sealed class PasswordStringExampleTests
{
    [Fact(DisplayName = "PasswordString Example")]
    public void Case1()
    {
        #region ExampleUsage

        PasswordString.TryParse("12Da3%sd", out var password, out _).Should().BeTrue();
        password.Value.Should().Be("12Da3%sd");

        #endregion
    }

    [Fact(DisplayName = "PasswordString source looks correct")]
    public Task Case2()
    {
        return """
            [Tuxedo.Refinement(
                "The string must be at least 8 characters long, contain at most 1 uppercase letter, number and special character. You provided '{value}'.",
                Name = nameof(PasswordString)
            )]
            internal static bool IsValidPassword(string value)
            """.VerifyRefinement();
    }
}
