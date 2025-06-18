using AutoMapper;
using Moq;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.Mappings;
using RentEase.Application.UnitTests.Mocks;
using RentEase.Infrastructure.Repositories;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands;

public class UpdateCarCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWorkMock;

    public UpdateCarCommandHandlerTests()
    {
        _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
        MockCarRepository.AddDataCarRepository(_unitOfWorkMock.Object.RentEaseDbContext);

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task UpdateCarCommand_InputCar_ReturnsTrue()
    {
        // Arrange
        var handler = new UpdateCarCommandHandler(_unitOfWorkMock.Object, _mapper);
        var command = new UpdateCarCommand(8001, "Toyota", "SUV");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task UpdateCarCommand_InvalidCarId_ReturnsError()
    {
        // Arrange
        var handler = new UpdateCarCommandHandler(_unitOfWorkMock.Object, _mapper);
        var command = new UpdateCarCommand(9999, "Toyota", "SUV"); // Assuming 9999 does not exist
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}