using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentEase.API.Controllers;
using RentEase.Application.Features.Rent.Commands;
using Xunit;

namespace RentEase.UnitTests.API.Controllers
{
    public class RentalControllerTests
    {
        [Fact]
        public async Task RegisterRental_ReturnsOkResult_WithExpectedValue()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var command = new RegisterRentalCommand(
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddDays(2),
                carId: 1
            );
            mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var controller = new RentalController(mediatorMock.Object);

            // Act
            var result = await controller.RegisterRental(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value!);
        }
    }
}
