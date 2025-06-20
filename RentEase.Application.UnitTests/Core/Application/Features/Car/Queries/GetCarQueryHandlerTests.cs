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
    public class GetCarQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public GetCarQueryHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            MockCarRepository.AddDataCarRepository(_unitOfWorkMock.Object.RentEaseDbContext);

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GerCarQuery_InputCar_ReturnsCar()
        {
            // Arrange
            var handler = new GetCarQueryHandler(_unitOfWorkMock.Object, _mapper);
            var command = new GetCarQuery(8001);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(8001);
            result.Model.ShouldBe("Toyota Corolla");
            result.Type.ShouldBe("Sedan");
        }

        [Fact]
        public async Task GetCarQuery_InvalidCarId_ReturnsError()
        {
            // Arrange
            var handler = new GetCarQueryHandler(_unitOfWorkMock.Object, _mapper);
            var command = new GetCarQuery(9999);
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}