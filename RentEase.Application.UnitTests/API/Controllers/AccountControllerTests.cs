using Microsoft.AspNetCore.Mvc;
using Moq;
using RentEase.Application.Contracts.Identity;
using RentEase.Application.Models.Identity;
using RentEase.API.Controllers;
using Xunit;

namespace RentEase.UnitTests.API.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AccountController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithAuthResponse()
        {
            // Arrange
            var request = new AuthRequest { Email = "test@example.com", Password = "password" };
            var expectedResponse = new AuthResponse
            {
                Id = "1",
                Username = "testuser",
                Email = "test@example.com",
                Token = "token"
            };
            _authServiceMock.Setup(s => s.LoginAsync(request)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Id, response.Id);
            Assert.Equal(expectedResponse.Username, response.Username);
            Assert.Equal(expectedResponse.Email, response.Email);
            Assert.Equal(expectedResponse.Token, response.Token);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WithRegistrationResponse()
        {
            // Arrange
            var request = new RegistrationRequest
            {
                Username = "testuser",
                FullName = "Test User",
                Address = "123 Main St",
                Email = "test@example.com",
                Password = "password"
            };
            var expectedResponse = new RegistrationResponse
            {
                Id = "1",
                Username = "testuser",
                Email = "test@example.com",
                Token = "token"
            };
            _authServiceMock.Setup(s => s.RegisterAsync(request)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<RegistrationResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Id, response.Id);
            Assert.Equal(expectedResponse.Username, response.Username);
            Assert.Equal(expectedResponse.Email, response.Email);
            Assert.Equal(expectedResponse.Token, response.Token);
        }
    }
}
