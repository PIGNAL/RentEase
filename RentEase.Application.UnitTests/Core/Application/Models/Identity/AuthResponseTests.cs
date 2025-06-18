using RentEase.Application.Models.Identity;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models.Identity;

public class AuthResponseTests
{
    [Fact]
    public void AuthResponse_DefaultValues_ShouldBeEmptyStrings()
    {
        // Arrange & Act
        var response = new AuthResponse();

        // Assert
        Assert.Equal(string.Empty, response.Id);
        Assert.Equal(string.Empty, response.Username);
        Assert.Equal(string.Empty, response.Email);
        Assert.Equal(string.Empty, response.Token);
    }

    [Fact]
    public void AuthResponse_SetProperties_ShouldStoreValues()
    {
        // Arrange
        var response = new AuthResponse
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