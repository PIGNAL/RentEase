using Microsoft.EntityFrameworkCore;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Domain.Common;
using RentEase.Infrastructure.Persistence;
using RentEase.Infrastructure.Repositories;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Infrastructure.Repositories
{
    public class TestEntity : BaseDomainModel
    {
        public string? Name { get; set; }
    }

    public class RepositoryBaseTests
    {
        private List<TestEntity> GetTestEntities() =>
            new List<TestEntity>
            {
            new TestEntity { Id = 1, Name = "Entity1" },
            new TestEntity { Id = 2, Name = "Entity2" }
            };

        private DbSet<TestEntity> CreateMockDbSet(List<TestEntity> data)
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<TestEntity>>();
            mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<TestEntity>())).Callback<TestEntity>(data.Add);
            mockSet.Setup(m => m.Remove(It.IsAny<TestEntity>())).Callback<TestEntity>(e => data.Remove(e));
            mockSet.Setup(m => m.Attach(It.IsAny<TestEntity>())).Callback<TestEntity>(e => { });
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(ids => new ValueTask<TestEntity?>(data.FirstOrDefault(e => e.Id == (int)ids[0])));
            return mockSet.Object;
        }

        private RentEaseDbContext CreateMockContext(List<TestEntity> data)
        {
            var options = new DbContextOptionsBuilder<RentEaseDbContext>().Options;
            var mockUserService = new Mock<ICurrentUserService>();
            var mockContext = new Mock<RentEaseDbContext>(options, mockUserService.Object);
            mockContext.Setup(c => c.Set<TestEntity>()).Returns(CreateMockDbSet(data));
            return mockContext.Object;
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectEntity()
        {
            var data = GetTestEntities();
            var context = CreateMockContext(data);
            var repo = new RepositoryBase<TestEntity>(context);

            var result = await repo.GetByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(2, result!.Id);
        }

        [Fact]
        public void AddEntity_AddsEntityToSet()
        {
            var data = GetTestEntities();
            var context = CreateMockContext(data);
            var repo = new RepositoryBase<TestEntity>(context);
            var newEntity = new TestEntity { Id = 3, Name = "Entity3" };

            repo.AddEntity(newEntity);

            Assert.Contains(newEntity, data);
        }

        [Fact]
        public void DeleteEntity_RemovesEntityFromSet()
        {
            var data = GetTestEntities();
            var context = CreateMockContext(data);
            var repo = new RepositoryBase<TestEntity>(context);
            var entity = data[0];

            repo.DeleteEntity(entity);

            Assert.DoesNotContain(entity, data);
        }
    }
}
