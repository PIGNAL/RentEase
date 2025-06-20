using AutoFixture;
using RentEase.Infrastructure.Persistence;
using RentEase.Domain;

namespace RentEase.Application.UnitTests.Mocks
{
    public static class MockCarRepository
    {
        public static void AddDataCarRepository(RentEaseDbContext rentEaseDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var cars = fixture.CreateMany<Car>().ToList();

            cars.Add(fixture.Build<Car>()
                .With(tr => tr.Id, 8001)
                .With(tr => tr.Model, "Toyota Corolla")
                .With(tr => tr.Type, "Sedan")
                .Create()
            );

            cars.Add(fixture.Build<Car>()
                .With(tr => tr.Id, 8002)
                .With(tr => tr.Model, "Toyota Etios")
                .With(tr => tr.Type, "Sedan")
                .Create()
            );

            cars.Add(fixture.Build<Car>()
                .With(tr => tr.Id, 8003)
                .With(tr => tr.Model, "Toyota Yaris")
                .With(tr => tr.Type, "Sedan")
                .Create()
            );

            rentEaseDbContextFake.Cars!.AddRange(cars);
            rentEaseDbContextFake.SaveChanges();
        }
    }
}
