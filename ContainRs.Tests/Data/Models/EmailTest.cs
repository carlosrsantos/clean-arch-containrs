using ContainRs.Domain.Models;

namespace ContainRs.Tests;

public class EmailTest
{
    [Fact]
    public void Assert_ThrowArgumentException_InvalidEmail()
    {
        var invalidEmail = "invalid-email";

        Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
    }

    [Fact]
    public void Assert_CreateEmail_ValidEmail()
    {
        var validEmail = "test@mail.com";

        var email = new Email(validEmail);
        Assert.Equal(validEmail, email.Value);
        
    }
}