using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class GetRentalQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public GetRentalQueryHandlerTests()
        {
            _unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            MockRentalRepository.AddDataRentalRepository(_unitOfWorkMock.Object.RentEaseDbContext);

            var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetRentalQuery_ValidRentalId_ReturnsRental()
        {
            // Arrange
            var rentalId = 9001;
            var handler = new GetRentalQueryHandler(_unitOfWorkMock.Object, _mapper);
            var command = new GetRentalQuery(rentalId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<RentalDto>();
            result.Id.ShouldBe(rentalId);
        }

        [Fact]
        public async Task GetRentalQuery_InvalidRentalId_ReturnsNull()
        {
            // Arrange
            var rentalId = 9999;
            var handler = new GetRentalQueryHandler(_unitOfWorkMock.Object, _mapper);
            var command = new GetRentalQuery(rentalId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
