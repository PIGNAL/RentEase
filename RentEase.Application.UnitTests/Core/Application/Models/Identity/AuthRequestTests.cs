using RentEase.Application.Models.Identity;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models.Identity
{
    public class AuthRequestTests
    {
        [Fact]
        public void AuthRequest_DefaultConstructor_InitializesProperties()
        {
            // Arrange & Act
            var authRequest = new AuthRequest();

            // Assert
            Assert.Equal(string.Empty, authRequest.Email);
            Assert.Equal(string.Empty, authRequest.Password);
        }

        [Fact]
        public void AuthRequest_SetProperties_AssignsValues()
        {
            // Arrange
            var authRequest = new AuthRequest();
            var email = "test@example.com";
            var password = "TestPassword123";

            // Act
            authRequest.Email = email;
            authRequest.Password = password;

            // Assert
            Assert.Equal(email, authRequest.Email);
            Assert.Equal(password, authRequest.Password);
        }
    }
}
