using Microsoft.EntityFrameworkCore;
using RentEase.Identity.Models;
using RentEase.Identity.Persistence;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Identity.Persistence
{
    public class IdentityDbContextTests
    {
        private DbContextOptions<IdentityDbContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIdentityDb")
                .Options;
        }

        [Fact]
        public void CanCreateDbContext()
        {
            var options = CreateInMemoryOptions();
            using var context = new IdentityDbContext(options);
            Assert.NotNull(context);
        }

        [Fact]
        public void CanAddAndRetrieveUser()
        {
            var options = CreateInMemoryOptions();
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = "test@example.com",
                FullName = "Test User",
                Address = "123 Test St"
            };

            using (var context = new IdentityDbContext(options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new IdentityDbContext(options))
            {
                var retrieved = context.Users.FirstOrDefault(u => u.UserName == "testuser");
                Assert.NotNull(retrieved);
                Assert.Equal("Test User", retrieved.FullName);
                Assert.Equal("123 Test St", retrieved.Address);
            }
        }
    }
}
