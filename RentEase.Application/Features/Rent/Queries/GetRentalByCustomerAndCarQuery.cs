using RentEase.Application.Models;
using MediatR;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalByCustomerAndCarQuery : IRequest<RentalDto>
    {
        public GetRentalByCustomerAndCarQuery(int customerId, int carId)
        {
            CustomerId = customerId;
            CarId = carId;
        }

        public int CustomerId { get; set; }
        public int CarId { get; set; }
        
    }
}
