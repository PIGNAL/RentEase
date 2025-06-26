using RentEase.Domain;

namespace RentEase.Application.Contracts.Persistence
{
    public interface IServiceRepository : IRepositoryBase<Service>
    {
        Task<Service?> GetLastServiceBeforeDateAsync(int carId, DateTime to);

    }
}
