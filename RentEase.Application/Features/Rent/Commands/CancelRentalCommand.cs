using MediatR;

namespace RentEase.Application.Features.Rent.Commands
{
    public class CancelRentalCommand : IRequest<bool>
    {
        public CancelRentalCommand(int customerId, int carId)
        {
            CustomerId = customerId;
            CarId = carId;
        }

        public int CustomerId { get; set; }
        public int CarId { get; set; }
    }
}
