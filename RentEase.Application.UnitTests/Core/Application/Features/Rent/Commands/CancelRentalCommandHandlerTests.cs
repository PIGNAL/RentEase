using Moq;
using RentEase.Application.Features.Rent.Commands;
using RentEase.Infrastructure.Repositories;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Rent.Commands;

public class CancelRentalCommandHandlerTests
{
    private readonly Mock<UnitOfWork> _unitOfWorkMock;
    public CancelRentalCommandHandlerTests()
    {
        _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
        MockRentalRepository.AddDataRentalRepository(_unitOfWorkMock.Object.RentEaseDbContext);
    }

    [Fact]
    public async Task CancelRentalCommand_ValidRentalId_ReturnsTrue()
    {
        // Arrange
        var handler = new CancelRentalCommandHandler(_unitOfWorkMock.Object);
        var command = new CancelRentalCommand(9001);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task CancelRentalCommand_InvalidRentalId_ReturnsError()
    {
        // Arrange
        var handler = new CancelRentalCommandHandler(_unitOfWorkMock.Object);
        var command = new CancelRentalCommand(9999);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBeFalse();
    }
}