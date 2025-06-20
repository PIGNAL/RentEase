using AutoMapper;
using Moq;
using RentEase.Application.Features.Car.Queries;
using RentEase.Application.Mappings;
using RentEase.Infrastructure.Repositories;
using RentEase.UnitTests.Mocks;
using Shouldly;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Queries
{
    public class GetAllCarsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public GetAllCarsQueryHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            MockCarRepository.AddDataCarRepository(_unitOfWorkMock.Object.RentEaseDbContext);

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllCarsQueryHandler_ShouldReturnAllCars()
        {
            // Arrange
            var handler = new GetAllCarsQueryHandler(_unitOfWorkMock.Object, _mapper);
            var query = new GetAllCarsQuery();
            // Act
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(6);
        }
    }
}
