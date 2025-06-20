using Microsoft.EntityFrameworkCore;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain;
using RentEase.Infrastructure.Persistence;

namespace RentEase.Infrastructure.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RentEaseDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Car>> GetAvailableCars(DateTime startDate, DateTime endDate)
        {
            var availableCars = await _context.Cars
                .Where(car =>
                    !car.IsDeleted &&
                    !_context.Rentals.Any(r =>
                        r.CarId == car.Id &&
                        r.StartDate < endDate &&
                        r.EndDate > startDate
                    ) &&
                    !car.Services.Any(s =>
                        s.Date >= startDate && s.Date <= endDate
                    )
                )
                .ToListAsync();

            return availableCars;
        }
    }
}
