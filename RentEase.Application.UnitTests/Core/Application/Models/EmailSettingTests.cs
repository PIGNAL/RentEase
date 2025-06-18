using RentEase.Application.Models;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Models
{
    public class EmailSettingTests
    {
        private readonly EmailSettings _emailSetting;

        [Fact]
        public void EmailSettings_DefaultValues_ShouldBeEmptyStrings()
        {
            // Arrange & Act
            var settings = new EmailSettings();

            // Assert
            Assert.Equal(string.Empty, settings.ApiKey);
            Assert.Equal(string.Empty, settings.FromAddress);
            Assert.Equal(string.Empty, settings.FromName);
        }

        [Fact]
        public void EmailSettings_SetProperties_ShouldStoreValues()
        {
            // Arrange
            var settings = new EmailSettings
            {
                ApiKey = "test-api-key",
                FromAddress = "test@example.com",
                FromName = "Test Sender"
            };

            // Assert
            Assert.Equal("test-api-key", settings.ApiKey);
            Assert.Equal("test@example.com", settings.FromAddress);
            Assert.Equal("Test Sender", settings.FromName);
        }
    }
}
