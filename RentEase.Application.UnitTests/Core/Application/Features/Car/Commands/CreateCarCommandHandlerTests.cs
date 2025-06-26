using AutoMapper;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Application.Features.Car.Commands;
using RentEase.Infrastructure.Repositories;
using RentEase.Application.Mappings;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands
{
    public class CreateCarCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICarMaintenanceService> _carMaintenanceServiceMock;

        public CreateCarCommandHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _carMaintenanceServiceMock = new Mock<ICarMaintenanceService>();

        }

        [Fact]
        public async Task CreateCarCommand_InputCar_ReturnsNumber()
        {
            // Arrange
            var handler = new CreateCarCommandHandler(_unitOfWorkMock.Object, _mapper, _carMaintenanceServiceMock.Object);
            var command = new CreateCarCommand("Toyota", "Sedan");
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<int>();
            result.ShouldBeGreaterThan(0);
        }
    }
}
