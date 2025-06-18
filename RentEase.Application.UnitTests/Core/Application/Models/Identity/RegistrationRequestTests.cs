using RentEase.Application.Models.Identity;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models.Identity;

public class RegistrationRequestTests
{
    [Fact]
    public void Constructor_SetsDefaultValues()
    {
        var request = new RegistrationRequest();

        Assert.Equal(string.Empty, request.Username);
        Assert.Equal(string.Empty, request.FullName);
        Assert.Equal(string.Empty, request.Address);
        Assert.Equal(string.Empty, request.Email);
        Assert.Equal(string.Empty, request.Password);
    }

    [Fact]
    public void Properties_CanBeSetAndRetrieved()
    {
        var request = new RegistrationRequest
        {
            Username = "user1",
            FullName = "John Doe",
            Address = "123 Main St",
            Email = "john@example.com",
            Password = "Secret123"
        };

        Assert.Equal("user1", request.Username);
        Assert.Equal("John Doe", request.FullName);
        Assert.Equal("123 Main St", request.Address);
        Assert.Equal("john@example.com", request.Email);
        Assert.Equal("Secret123", request.Password);
    }
}