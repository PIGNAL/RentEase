using AutoMapper;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Features.Rent.Commands;
using RentEase.Application.Mappings;
using RentEase.Infrastructure.Repositories;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Rent.Commands;

public class UpdateRentalCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;

    public UpdateRentalCommandHandlerTests()
    {
        _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
        MockRentalRepository.AddDataRentalRepository(_unitOfWorkMock.Object.RentEaseDbContext);

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();


        _emailServiceMock = new Mock<IEmailService>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
    }

    [Fact]
    public async Task UpdateRentalCommand_InputRental_ReturnsTrue()
    {
        // Arrange
        var handler = new UpdateRentalCommandHandler(_unitOfWorkMock.Object, _mapper, _emailServiceMock.Object, _currentUserServiceMock.Object);
        var command = new UpdateRentalCommand(9001, 8001, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(2));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task UpdateRentalCommand_InvalidRentalId_ReturnsError()
    {
        // Arrange
        var handler = new UpdateRentalCommandHandler(_unitOfWorkMock.Object, _mapper, _emailServiceMock.Object, _currentUserServiceMock.Object);
        var command = new UpdateRentalCommand(9999, 8001, DateTime.UtcNow, DateTime.UtcNow.AddDays(1)); // Assuming 9999 does not exist
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}