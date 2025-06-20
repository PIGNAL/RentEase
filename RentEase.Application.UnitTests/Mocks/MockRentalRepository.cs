using AutoFixture;
using RentEase.Domain;
using RentEase.Infrastructure.Persistence;

namespace RentEase.UnitTests.Mocks
{
    public static class MockRentalRepository
    {
        public static void AddDataRentalRepository(RentEaseDbContext rentEaseDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var rentals = fixture.CreateMany<Rental>().ToList();

            rentals.Add(fixture.Build<Rental>()
                .With(r => r.Id, 9001)
                .With(r => r.CarId, 8001)
                .With(r => r.CustomerId, 1001)
                .With(r => r.StartDate, DateTime.Today.AddDays(-10))
                .With(r => r.EndDate, DateTime.Today.AddDays(-5))
                .With(r => r.Customer, new Customer() { Email = "usertests@gmail.com", Id = 9001, Address = "test", FullName = "tests fullname" })
                .With(r => r.IsDeleted, false)
                .Create()
            );

            rentals.Add(fixture.Build<Rental>()
                .With(r => r.Id, 9002)
                .With(r => r.CarId, 8002)
                .With(r => r.CustomerId, 1002)
                .With(r => r.StartDate, DateTime.Today.AddDays(-7))
                .With(r => r.EndDate, DateTime.Today.AddDays(-2))
                .Create()
            );

            rentals.Add(fixture.Build<Rental>()
                .With(r => r.Id, 9003)
                .With(r => r.CarId, 8003)
                .With(r => r.CustomerId, 1003)
                .With(r => r.StartDate, DateTime.Today.AddDays(-3))
                .With(r => r.EndDate, DateTime.Today)
                .Create()
            );

            rentEaseDbContextFake.Rentals!.AddRange(rentals);
            rentEaseDbContextFake.SaveChanges();
        }
    }
}
