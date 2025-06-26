using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain;
using RentEase.Domain.Services;

namespace RentEase.Infrastructure.Services
{
    public class CarMaintenanceService : ICarMaintenanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CarMaintenanceScheduler _scheduler;

        public CarMaintenanceService(IUnitOfWork unitOfWork, CarMaintenanceScheduler scheduler)
        {
            _unitOfWork = unitOfWork;
            _scheduler = scheduler;
        }

        public async Task ScheduleServicesForCar(Car car, DateTime from, DateTime to)
        {
            var lastService = await _unitOfWork.ServiceRepository.GetLastServiceBeforeDateAsync(car.Id, to);

            if (lastService != null && lastService.Date.Date.AddMonths(2) > from.Date)
            {
                return;
            }

            var services = _scheduler.GenerateScheduledServices(car, from, to);

            foreach (var service in services)
            {
                _unitOfWork.Repository<Service>().AddEntity(service);
            }
            await _unitOfWork.Complete();
        }

        public async Task ScheduleServicesForAllCars(DateTime from, DateTime to)
        {
            var cars = await _unitOfWork.Repository<Car>().GetAllAsync();
            foreach (var car in cars)
            {
                await ScheduleServicesForCar(car, from, to);
            }
        }
    }
}
