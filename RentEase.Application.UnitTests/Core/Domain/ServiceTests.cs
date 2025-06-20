using RentEase.Domain;
using Xunit;

namespace RentEase.UnitTests.Core.Domain
{
    public class ServiceTests
    {
        [Fact]
        public void Can_Create_Service_With_Valid_Properties()
        {
            // Arrange
            var date = new DateTime(2024, 6, 1);
            var carId = 42;
            var car = new Car { Id = carId, Type = "SUV", Model = "X5" };

            // Act
            var service = new Service
            {
                Date = date,
                CarId = carId,
                Car = car
            };

            // Assert
            Assert.Equal(date, service.Date);
            Assert.Equal(carId, service.CarId);
            Assert.Equal(car, service.Car);
        }

        [Fact]
        public void Can_Set_And_Get_Car_Null()
        {
            // Arrange
            var service = new Service();

            // Act
            service.Car = null;

            // Assert
            Assert.Null(service.Car);
        }
    }
}
