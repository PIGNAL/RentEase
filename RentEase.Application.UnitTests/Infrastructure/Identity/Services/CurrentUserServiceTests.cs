using Microsoft.AspNetCore.Http;
using Moq;
using RentEase.Identity.Services;
using System.Security.Claims;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Identity.Services
{

    public class CurrentUserServiceTests
    {
        private static DefaultHttpContext CreateHttpContextWithClaims(params Claim[] claims)
        {
            var context = new DefaultHttpContext();
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            context.User = new ClaimsPrincipal(identity);
            return context;
        }

        [Fact]
        public void UserName_ReturnsClaimValue_WhenClaimExists()
        {
            var httpContext = CreateHttpContextWithClaims(new Claim(ClaimTypes.Name, "testuser"));
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            Assert.Equal("testuser", service.UserName);
        }

        [Fact]
        public void Email_ReturnsClaimValue_WhenClaimExists()
        {
            var httpContext = CreateHttpContextWithClaims(new Claim(ClaimTypes.Email, "test@email.com"));
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            Assert.Equal("test@email.com", service.Email);
        }

        [Fact]
        public void FullName_ReturnsClaimValue_WhenClaimExists()
        {
            var httpContext = CreateHttpContextWithClaims(new Claim(ClaimTypes.GivenName, "John Doe"));
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            Assert.Equal("John Doe", service.FullName);
        }

        [Fact]
        public void Address_ReturnsClaimValue_WhenClaimExists()
        {
            var httpContext = CreateHttpContextWithClaims(new Claim(ClaimTypes.StreetAddress, "123 Main St"));
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            Assert.Equal("123 Main St", service.Address);
        }

        [Fact]
        public void Properties_ReturnEmptyString_WhenClaimDoesNotExist()
        {
            var httpContext = CreateHttpContextWithClaims();
            var accessorMock = new Mock<IHttpContextAccessor>();
            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            var service = new CurrentUserService(accessorMock.Object);

            Assert.Equal(string.Empty, service.UserName);
            Assert.Equal(string.Empty, service.Email);
            Assert.Equal(string.Empty, service.FullName);
            Assert.Equal(string.Empty, service.Address);
        }
    }
}
