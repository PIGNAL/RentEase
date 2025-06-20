using MediatR;

namespace RentEase.Application.Features.Rent.Commands
{
    public class UpdateRentalCommand: IRequest<bool>
    {
        public UpdateRentalCommand(DateTime startDate, DateTime endDate, int carId)
        {
            StartDate = startDate;
            EndDate = endDate;
            CarId = carId;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
    }
}
