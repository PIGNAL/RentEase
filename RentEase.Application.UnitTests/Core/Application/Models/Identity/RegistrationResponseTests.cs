using RentEase.Application.Models.Identity;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models.Identity;

public class RegistrationResponseTests
{
    [Fact]
    public void RegistrationResponse_DefaultValues_ShouldBeEmptyStrings()
    {
        // Arrange
        var response = new RegistrationResponse();

        // Assert
        Assert.Equal(string.Empty, response.Id);
        Assert.Equal(string.Empty, response.Username);
        Assert.Equal(string.Empty, response.Email);
        Assert.Equal(string.Empty, response.Token);
    }

    [Fact]
    public void RegistrationResponse_SetProperties_ShouldStoreValues()
    {
        // Arrange
        var response = new RegistrationResponse
        {
            Id = "123",
            Username = "testuser",
            Email = "test@example.com",
            Token = "token123"
        };

        // Assert
        Assert.Equal("123", response.Id);
        Assert.Equal("testuser", response.Username);
        Assert.Equal("test@example.com", response.Email);
        Assert.Equal("token123", response.Token);
    }
}