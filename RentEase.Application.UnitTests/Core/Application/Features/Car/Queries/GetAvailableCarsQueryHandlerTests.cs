using AutoMapper;
using Moq;
using RentEase.Application.Features.Car.Queries;
using RentEase.Infrastructure.Repositories;
using RentEase.Application.Mappings;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Queries
{
    public class GetAvailableCarsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public GetAvailableCarsQueryHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            MockCarRepository.AddDataCarRepository(_unitOfWorkMock.Object.RentEaseDbContext);

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAvailableCarsQueryHandler_ShouldReturnAvailableCars()
        {
            // Arrange
            var handler = new GetAvailableCarsQueryHandler(_unitOfWorkMock.Object, _mapper);
            var query = new GetAvailableCarsQuery(DateTime.Now, DateTime.Now.AddDays(7));
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result[0].ShouldNotBeNull();
            result[0].Type.ShouldNotBeNullOrEmpty();
        }
    }
}
