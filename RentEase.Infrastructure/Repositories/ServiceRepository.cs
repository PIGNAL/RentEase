using Microsoft.EntityFrameworkCore;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain;
using RentEase.Infrastructure.Persistence;

namespace RentEase.Infrastructure.Repositories
{
    internal class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(RentEaseDbContext context) : base(context)
        {
        }

        public async Task<Service?> GetLastServiceBeforeDateAsync(int carId, DateTime to)
        {
            return await _context.Services
                .Where(s => s.CarId == carId && s.Date.Date < to.Date)
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync();
        }
    }
}
