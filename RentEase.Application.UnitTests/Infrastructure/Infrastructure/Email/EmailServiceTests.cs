using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RentEase.Application.Models;
using RentEase.Infrastructure.Email;
using SendGrid;
using System.Net;
using SendGrid.Helpers.Mail;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Infrastructure.Email
{
    public class EmailServiceTests
    {
        private readonly Mock<IOptions<EmailSettings>> _mockOptions;
        private readonly Mock<ILogger<EmailService>> _mockLogger;
        private readonly EmailSettings _settings;

        public EmailServiceTests()
        {
            _settings = new EmailSettings
            {
                FromAddress= "joni_ballatore@hotmail.com",
                ApiKey= "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",
                FromName= "Jonathan Ballatore"
            };
            _mockOptions = new Mock<IOptions<EmailSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(_settings);
            _mockLogger = new Mock<ILogger<EmailService>>();
        }

        [Fact]
        public async Task SendEmail_ReturnsTrue_OnSuccess()
        {
            // Arrange
            var email = new Application.Models.Email
            {
                To = "to@test.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            var mockSendGridClient = new Mock<ISendGridClient>();
            var response = new Response(HttpStatusCode.Accepted, null, null);
            mockSendGridClient
                .Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), CancellationToken.None))
                .ReturnsAsync(response);

            var service = new EmailService(_mockOptions.Object, _mockLogger.Object);

            // Act
            var result = await service.SendEmail(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SendEmail_ReturnsFalse_OnFailure()
        {
            // Arrange
            var email = new Application.Models.Email
            {
                To = "",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            var service = new EmailService(_mockOptions.Object, _mockLogger.Object);

            // Act
            var result = await service.SendEmail(email);

            // Assert
            Assert.False(result);
        }
    }
}
