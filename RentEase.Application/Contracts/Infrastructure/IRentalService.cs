using MediatR;
using RentEase.Domain;

namespace RentEase.Application.Contracts.Infrastructure
{
    public interface IRentalService
    {
        public Task<IEnumerable<Car>> CheckAvailability(DateTime date, string carType, string carModel);
        public Task<Unit> RegisterRental(Rental rental);
        public Task<Unit> CancelRental();
        public Task<Unit> ModifyReservation();
    }
}
