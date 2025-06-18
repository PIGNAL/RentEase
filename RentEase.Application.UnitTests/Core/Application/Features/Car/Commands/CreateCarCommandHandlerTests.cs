using AutoMapper;
using Moq;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.UnitTests.Mocks;
using RentEase.Infrastructure.Repositories;
using RentEase.Application.Mappings;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands
{
    public class CreateCarCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public CreateCarCommandHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task CreateCarCommand_InputCar_ReturnsNumber()
        {
            // Arrange
            var handler = new CreateCarCommandHandler(_unitOfWorkMock.Object, _mapper);
            var command = new CreateCarCommand("Toyota", "Sedan");
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<int>();
            result.ShouldBeGreaterThan(0);
        }
    }
}
