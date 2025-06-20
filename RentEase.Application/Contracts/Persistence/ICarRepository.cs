using RentEase.Domain;

namespace RentEase.Application.Contracts.Persistence
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        Task<IEnumerable<Car>> GetAvailableCars(DateTime startDate, DateTime endDate);
    }
}
