using Moq;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.UnitTests.Mocks;
using RentEase.Infrastructure.Repositories;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands;

public class DeleteCarCommandHandlerTests
{
    private readonly Mock<UnitOfWork> _unitOfWorkMock;
    public DeleteCarCommandHandlerTests()
    {
        _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
        MockCarRepository.AddDataCarRepository(_unitOfWorkMock.Object.RentEaseDbContext);
    }

    [Fact]
    public async Task DeleteCarCommand_ValidCarId_ReturnsTrue()
    {
        // Arrange
        var handler = new DeleteCarCommandHandler(_unitOfWorkMock.Object);
        var command = new DeleteCarCommand(8001);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteCarCommand_InvalidCarId_ReturnsError()
    {
        // Arrange
        var handler = new DeleteCarCommandHandler(_unitOfWorkMock.Object);
        var command = new DeleteCarCommand(9999); 
        
        // Act
        var result =await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeFalse();
    }
}