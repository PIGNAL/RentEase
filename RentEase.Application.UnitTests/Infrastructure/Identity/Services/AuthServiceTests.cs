using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using RentEase.Application.Models.Identity;
using RentEase.Identity.Models;
using RentEase.Identity.Services;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Identity.Services;

public class AuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            contextAccessor.Object,
            userPrincipalFactory.Object,
            null, null, null, null
        );
        _jwtSettings = Options.Create(new JwtSettings
        {
            Key = "testkeytestkeytestkeytestkeytestkey12",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            DurationInMinutes = 60
        });
        _authService = new AuthService(_userManagerMock.Object, _signInManagerMock.Object, _jwtSettings);
    }

    [Fact]
    public async Task LoginAsync_ReturnsAuthResponse_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new ApplicationUser
        {
            Id = "testkeytestkeytestkeytestkeytestkey12",
            UserName = "testuser",
            Email = "test@example.com",
            FullName = "Test User",
            Address = "Test Address"
        };
        var request = new AuthRequest
        {
            Email = user.Email,
            Password = "Password123!"
        };

        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, request.Password, false, false))
            .ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(x => x.GetClaimsAsync(user)).ReturnsAsync(new List<Claim>());
        _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string>());

        // Act
        var result = await _authService.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.UserName, result.Username);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    [Fact]
    public async Task LoginAsync_ThrowsException_WhenUserNotFound()
    {
        // Arrange
        var request = new AuthRequest { Email = "notfound@example.com", Password = "Password123!" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(request));
        Assert.Contains("is null", ex.Message);
    }

    [Fact]
    public async Task LoginAsync_ThrowsException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new ApplicationUser { Id = "1", UserName = "testuser", Email = "test@example.com" };
        var request = new AuthRequest { Email = user.Email, Password = "wrongpassword" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, request.Password, false, false))
            .ReturnsAsync(SignInResult.Failed);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(request));
        Assert.Contains("incorrect", ex.Message);
    }

    [Fact]
    public async Task RegisterAsync_ReturnsRegistrationResponse_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var request = new RegistrationRequest
        {
            Username = "newuser",
            Email = "newuser@example.com",
            FullName = "New User",
            Address = "New Address",
            Password = "Password123!"
        };
        _userManagerMock.Setup(x => x.FindByNameAsync(request.Username)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(x => x.GetClaimsAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<Claim>());
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string>());
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Operator"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.RegisterAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Username, result.Username);
        Assert.Equal(request.Email, result.Email);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    [Fact]
    public async Task RegisterAsync_ThrowsException_WhenUsernameExists()
    {
        // Arrange
        var request = new RegistrationRequest
        {
            Username = "existinguser",
            Email = "newuser@example.com",
            FullName = "New User",
            Address = "New Address",
            Password = "Password123!"
        };
        _userManagerMock.Setup(x => x.FindByNameAsync(request.Username)).ReturnsAsync(new ApplicationUser());

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(request));
        Assert.Contains("already exists", ex.Message);
    }

    [Fact]
    public async Task RegisterAsync_ThrowsException_WhenEmailExists()
    {
        // Arrange
        var request = new RegistrationRequest
        {
            Username = "newuser",
            Email = "existing@example.com",
            FullName = "New User",
            Address = "New Address",
            Password = "Password123!"
        };
        _userManagerMock.Setup(x => x.FindByNameAsync(request.Username)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(new ApplicationUser());

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(request));
        Assert.Contains("already exists", ex.Message);
    }

    [Fact]
    public async Task RegisterAsync_ThrowsException_WhenUserCreationFails()
    {
        // Arrange
        var request = new RegistrationRequest
        {
            Username = "newuser",
            Email = "newuser@example.com",
            FullName = "New User",
            Address = "New Address",
            Password = "Password123!"
        };
        _userManagerMock.Setup(x => x.FindByNameAsync(request.Username)).ReturnsAsync((ApplicationUser)null);
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);
        var identityResult = IdentityResult.Failed(new IdentityError { Description = "Password too weak" });
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password))
            .ReturnsAsync(identityResult);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(request));
        Assert.Contains("could not be created", ex.Message);
        Assert.Contains("Password too weak", ex.Message);
    }
}