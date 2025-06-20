using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RentEase.API.Controllers;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.Features.Car.Queries;
using RentEase.Application.Models;
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

        [Fact]
        public async Task GetCarById_ReturnsOkResult_WithCarDto()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var carId = 1;
            var carDto = new CarDto { Id = carId, Model = "ModelX", Type = "SUV" };
            mediatorMock
                .Setup(m => m.Send(It.Is<GetCarQuery>(q => q.Id == carId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carDto);

            var controller = new CarController(mediatorMock.Object);

            // Act
            var result = await controller.GetCarById(carId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCar = Assert.IsType<CarDto>(okResult.Value);
            Assert.Equal(carDto.Id, returnedCar.Id);
            Assert.Equal(carDto.Model, returnedCar.Model);
            Assert.Equal(carDto.Type, returnedCar.Type);
        }

        [Fact]
        public async Task GetAllCars_ReturnsOkResult_WithListOfCarDto()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var carDtos = new List<CarDto>
            {
                new CarDto { Id = 1, Model = "ModelX", Type = "SUV" },
                new CarDto { Id = 2, Model = "ModelY", Type = "Sedan" }
            };
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllCarsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carDtos);
            var controller = new CarController(mediatorMock.Object);
            // Act
            var result = await controller.GetAllCars();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCars = Assert.IsType<List<CarDto>>(okResult.Value);
            Assert.Equal(carDtos.Count, returnedCars.Count);
        }

        [Fact]
        public async Task GetAllCars_ReturnsEmptyList_WhenNoCarsAvailable()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllCarsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CarDto>());
            var controller = new CarController(mediatorMock.Object);
            // Act
            var result = await controller.GetAllCars();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCars = Assert.IsType<List<CarDto>>(okResult.Value);
            Assert.Empty(returnedCars);
        }

        [Fact]
        public async Task GetAvailableCars_ReturnsOkResult_WithListOfCarDto()
        {
            // Arrange
            var startDate = new DateTime(2024, 7, 1);
            var endDate = new DateTime(2024, 7, 10);
            var cars = new List<CarDto>
            {
                new CarDto { Id = 1, Model = "ModelX", Type = "SUV" },
                new CarDto { Id = 2, Model = "ModelY", Type = "Sedan" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetAvailableCarsQuery>(q => q.StartDate == startDate && q.EndDate == endDate), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars);

            // Act
            var result = await _controller.GetAvailableCars(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCars = Assert.IsAssignableFrom<IEnumerable<CarDto>>(okResult.Value);
            Assert.Collection(returnedCars,
                car =>
                {
                    Assert.Equal(1, car.Id);
                    Assert.Equal("ModelX", car.Model);
                    Assert.Equal("SUV", car.Type);
                },
                car =>
                {
                    Assert.Equal(2, car.Id);
                    Assert.Equal("ModelY", car.Model);
                    Assert.Equal("Sedan", car.Type);
                });
        }
    }
}
