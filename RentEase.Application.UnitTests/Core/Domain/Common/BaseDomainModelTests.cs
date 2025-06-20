using RentEase.Domain.Common;
using Xunit;

namespace RentEase.UnitTests.Core.Domain.Common
{
    public class BaseDomainModelTests
    {
        private class TestDomainModel : BaseDomainModel { }

        [Fact]
        public void Default_Values_Are_Set_Correctly()
        {
            var model = new TestDomainModel();

            Assert.Equal(0, model.Id);
            Assert.Null(model.CreatedDate);
            Assert.Null(model.CreatedBy);
            Assert.Null(model.LastModifiedDate);
            Assert.Null(model.LastModifiedBy);
            Assert.False(model.IsDeleted);
            Assert.Null(model.DeletedDate);
            Assert.Null(model.DeletedBy);
        }

        [Fact]
        public void Properties_Can_Be_Set_And_Retrieved()
        {
            var now = DateTime.UtcNow;
            var model = new TestDomainModel
            {
                Id = 42,
                CreatedDate = now,
                CreatedBy = "user1",
                LastModifiedDate = now.AddMinutes(1),
                LastModifiedBy = "user2",
                IsDeleted = true,
                DeletedDate = now.AddMinutes(2),
                DeletedBy = "user3"
            };

            Assert.Equal(42, model.Id);
            Assert.Equal(now, model.CreatedDate);
            Assert.Equal("user1", model.CreatedBy);
            Assert.Equal(now.AddMinutes(1), model.LastModifiedDate);
            Assert.Equal("user2", model.LastModifiedBy);
            Assert.True(model.IsDeleted);
            Assert.Equal(now.AddMinutes(2), model.DeletedDate);
            Assert.Equal("user3", model.DeletedBy);
        }
    }
}
