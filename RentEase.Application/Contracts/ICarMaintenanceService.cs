using RentEase.Domain;

namespace RentEase.Application.Contracts
{
    public interface ICarMaintenanceService
    {
        Task ScheduleServicesForCar(Car car, DateTime from, DateTime to);
        Task ScheduleServicesForAllCars(DateTime from, DateTime to);
    }
}
