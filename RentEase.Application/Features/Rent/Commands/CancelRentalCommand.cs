using MediatR;

namespace RentEase.Application.Features.Rent.Commands
{
    public class CancelRentalCommand : IRequest<bool>
    {
        public CancelRentalCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
