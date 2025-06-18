using RentEase.Application.Models.Identity;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models.Identity;

public class JwtSettingsTests
{
    [Fact]
    public void JwtSettings_DefaultValues_ShouldBeEmptyOrZero()
    {
        // Arrange
        var settings = new JwtSettings();

        // Assert
        Assert.Equal(string.Empty, settings.Key);
        Assert.Equal(string.Empty, settings.Issuer);
        Assert.Equal(string.Empty, settings.Audience);
        Assert.Equal(0, settings.DurationInMinutes);
    }

    [Fact]
    public void JwtSettings_SetProperties_ShouldStoreValues()
    {
        // Arrange
        var settings = new JwtSettings
        {
            Key = "test-key",
            Issuer = "test-issuer",
            Audience = "test-audience",
            DurationInMinutes = 60
        };

        // Assert
        Assert.Equal("test-key", settings.Key);
        Assert.Equal("test-issuer", settings.Issuer);
        Assert.Equal("test-audience", settings.Audience);
        Assert.Equal(60, settings.DurationInMinutes);
    }
}