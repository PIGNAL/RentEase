using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentEase.API.Controllers;
using RentEase.Application.Features.Car.Commands;
using Xunit;

namespace RentEase.UnitTests.API.Controllers
{
    public class CarControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CarController _controller;

        public CarControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CarController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateCar_ReturnsOk_WithCarId()
        {
            // Arrange
            var command = new CreateCarCommand("ModelX", "SUV");
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateCar(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task UpdateCar_ReturnsOk_WithTrue()
        {
            // Arrange
            var command = new UpdateCarCommand(1, "ModelY", "Sedan");
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateCar(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task DeleteCar_ReturnsOk_WithTrue()
        {
            // Arrange
            var id = 2;
            _mediatorMock.Setup(m => m.Send(It.Is<DeleteCarCommand>(c => c.Id == id), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCar(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }
    }
}
