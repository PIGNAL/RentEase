using AutoMapper;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Application.Features.Rent.Queries;
using RentEase.Application.Mappings;
using RentEase.Application.Models;
using RentEase.Infrastructure.Repositories;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Rent.Queries
{
    public class GetRentalsByUserQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;

        public GetRentalsByUserQueryHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            MockRentalRepository.AddDataRentalRepository(_unitOfWorkMock.Object.RentEaseDbContext);

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task GetRentalsByUserQuery_ValidUserId_ReturnsRentals()
        {
            // Arrange
            _currentUserServiceMock.Setup(x => x.Email).Returns("usertests@gmail.com");
            var handler = new GetRentalsByUserQueryHandler(_unitOfWorkMock.Object, _mapper, _currentUserServiceMock.Object);
            var command = new GetRentalsByUserQuery(); 

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<IEnumerable<RentalDto>>();
            result.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GetRentalsByUserQuery_InvalidUserId_ReturnsEmpty()
        {
            // Arrange
            var handler = new GetRentalsByUserQueryHandler(_unitOfWorkMock.Object, _mapper, _currentUserServiceMock.Object);
            var command = new GetRentalsByUserQuery();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }
    }
}
