using Moq;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain;
using RentEase.Domain.Services;
using RentEase.Infrastructure.Services;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Infrastructure.Services
{
    public class CarMaintenanceServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IServiceRepository> _serviceRepoMock;
        private readonly Mock<IRepositoryBase<Service>> _serviceBaseRepoMock;
        private readonly Mock<IRepositoryBase<Car>> _carBaseRepoMock;
        private readonly CarMaintenanceScheduler _scheduler;
        private readonly CarMaintenanceService _service;

        public CarMaintenanceServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _serviceRepoMock = new Mock<IServiceRepository>();
            _serviceBaseRepoMock = new Mock<IRepositoryBase<Service>>();
            _carBaseRepoMock = new Mock<IRepositoryBase<Car>>();
            _scheduler = new CarMaintenanceSchedulerStub();

            _unitOfWorkMock.Setup(u => u.ServiceRepository).Returns(_serviceRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.Repository<Service>()).Returns(_serviceBaseRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.Repository<Car>()).Returns(_carBaseRepoMock.Object);

            _service = new CarMaintenanceService(_unitOfWorkMock.Object, _scheduler);
        }

        [Fact]
        public async Task ScheduleServicesForCar_ShouldNotSchedule_WhenLastServiceIsRecent()
        {
            var car = new Car { Id = 1 };
            var lastService = new Service { Date = DateTime.Today.AddMonths(-1) };
            _serviceRepoMock.Setup(r => r.GetLastServiceBeforeDateAsync(car.Id, It.IsAny<DateTime>()))
                .ReturnsAsync(lastService);

            await _service.ScheduleServicesForCar(car, DateTime.Today, DateTime.Today.AddMonths(1));

            _serviceBaseRepoMock.Verify(r => r.AddEntity(It.IsAny<Service>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Never);
        }

        [Fact]
        public async Task ScheduleServicesForCar_ShouldSchedule_WhenNoRecentService()
        {
            var car = new Car { Id = 1 };
            _serviceRepoMock.Setup(r => r.GetLastServiceBeforeDateAsync(car.Id, It.IsAny<DateTime>()))
                .ReturnsAsync((Service?)null);

            await _service.ScheduleServicesForCar(car, DateTime.Today, DateTime.Today.AddMonths(1));

            _serviceBaseRepoMock.Verify(r => r.AddEntity(It.IsAny<Service>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public async Task ScheduleServicesForAllCars_ShouldScheduleForEachCar()
        {
            var cars = new List<Car>
            {
                new Car { Id = 1 },
                new Car { Id = 2 }
            };
            _carBaseRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cars);
            _serviceRepoMock.Setup(r => r.GetLastServiceBeforeDateAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync((Service?)null);

            await _service.ScheduleServicesForAllCars(DateTime.Today, DateTime.Today.AddMonths(1));

            _serviceBaseRepoMock.Verify(r => r.AddEntity(It.IsAny<Service>()), Times.Exactly(cars.Count));
            _unitOfWorkMock.Verify(u => u.Complete(), Times.Exactly(cars.Count));
        }

        // Stub for CarMaintenanceScheduler
        private class CarMaintenanceSchedulerStub : CarMaintenanceScheduler
        {
            public override IEnumerable<Service> GenerateScheduledServices(Car car, DateTime from, DateTime to)
            {
                return new List<Service>
                {
                    new Service { CarId = car.Id, Date = from }
                };
            }
        }
    }
}
