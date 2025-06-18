using Microsoft.EntityFrameworkCore;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Persistence;
using RentEase.Infrastructure.Persistence;
using RentEase.Infrastructure.Repositories;

namespace RentEase.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            var dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<RentEaseDbContext>()
                .UseInMemoryDatabase(databaseName: $"RentEaseDbContext-{dbContextId}")
                .Options;
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            var rentEaseDbContextFake = new RentEaseDbContext(options, currentUserServiceMock.Object);
            rentEaseDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(rentEaseDbContextFake);


            return mockUnitOfWork;
        }

    }
}
