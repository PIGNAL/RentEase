
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

namespace RentEase.UnitTests.Core.Application.Features.Rent.Commands
{
    public class RegisterRentalCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;

        public RegisterRentalCommandHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            _emailServiceMock = new Mock<IEmailService>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task RegisterRental_ShouldReturnRentalId_WhenSuccessful()
        {
            // Arrange
            var handler = new RegisterRentalCommandHandler(_unitOfWorkMock.Object, _mapper, _emailServiceMock.Object, _currentUserServiceMock.Object);
            var command = new RegisterRentalCommand(DateTime.UtcNow, DateTime.UtcNow.AddDays(7), 1);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            result.ShouldBeTrue();
        }
    }
}
