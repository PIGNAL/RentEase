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
        public async Task RegisterRental_ReturnsOkResult_WithTrue()
        {
            // Arrange
            var command = new RegisterRentalCommand(null, null, null);
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.RegisterRental(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task UpdateRental_ReturnsOkResult_WithTrue()
        {
            // Arrange
            var command = new UpdateRentalCommand(1,2, DateTime.Now, DateTime.Now.AddDays(1));
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateRental(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task CancelRental_ReturnsOkResult_WithTrue()
        {
            // Arrange
            int id = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelRentalCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.CancelRental(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task GetRentals_ReturnsOkResult_WithRentalList()
        {
            // Arrange
            var rentals = new List<RentalDto>
            {
                new RentalDto { Id = 1, CarId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Car = new CarDto { Id = 2, Model = "ModelX", Type = "SUV" } }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalsByUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(rentals);

            // Act
            var result = await _controller.GetRentals();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<RentalDto>>(okResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetRentalByCustomerAndCar_ReturnsOkResult_WithRental()
        {
            // Arrange
            int id = 1;
            var rental = new RentalDto { Id = id, CarId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Car = new CarDto { Id = 2, Model = "ModelX", Type = "SUV" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(rental);

            // Act
            var result = await _controller.GetRentalByCustomerAndCar(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<RentalDto>(okResult.Value);
            Assert.Equal(id, value.Id);
        }

        [Fact]
        public async Task GetRentalByCustomerAndCar_ReturnsNotFound_WhenRentalIsNull()
        {
            // Arrange
            int id = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((RentalDto)null);

            // Act
            var result = await _controller.GetRentalByCustomerAndCar(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

