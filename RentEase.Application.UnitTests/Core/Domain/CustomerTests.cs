using RentEase.Domain;
using Xunit;

namespace RentEase.UnitTests.Core.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Properties_SetAndGetValues()
        {
            // Arrange
            var customer = new Customer();

            // Act
            customer.FullName = "John Doe";
            customer.Address = "123 Main St";
            customer.Email = "john.doe@example.com";

            // Assert
            Assert.Equal("John Doe", customer.FullName);
            Assert.Equal("123 Main St", customer.Address);
            Assert.Equal("john.doe@example.com", customer.Email);
        }

        [Fact]
        public void Customer_DefaultValues_AreNull()
        {
            // Arrange
            var customer = new Customer();

            // Assert
            Assert.Null(customer.FullName);
            Assert.Null(customer.Address);
            Assert.Null(customer.Email);
        }
    }
}
