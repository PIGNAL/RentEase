using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentEase.API.Controllers;
using RentEase.Application.Features.Rent.Commands;
using RentEase.Application.Features.Rent.Queries;
using RentEase.Application.Models;
using Xunit;

namespace RentEase.UnitTests.API.Controllers
{
    public class RentalControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RentalController _controller;

        public RentalControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RentalController(_mediatorMock.Object);
        }

        [Fact]
        public async Task RegisterRental_ReturnsOkResult_WithExpectedValue()
        {
            // Arrange
            var command = new RegisterRentalCommand(
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddDays(2),
                carId: 1
            );
            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.RegisterRental(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value!);
        }

        [Fact]
        public async Task GetRentals_ReturnsOkResult_WithListOfRentalDto()
        {
            // Arrange
            var rentals = new List<RentalDto>
            {
                new RentalDto
                {
                    CustomerId = 1,
                    CarId = 2,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(2),
                    Car = new CarDto { Id = 2, Model = "ModelX", Type = "SUV" }
                }
            };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetRentalsByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(rentals);

            // Act
            var result = await _controller.GetRentals();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<RentalDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal(1, returnValue.First().CustomerId);
            Assert.Equal(2, returnValue.First().CarId);
            Assert.Equal("ModelX", returnValue.First().Car.Model);
            Assert.Equal("SUV", returnValue.First().Car.Type);
        }
    }
}

