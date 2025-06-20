using MediatR;

namespace RentEase.Application.Features.Rent.Commands
{
    public class UpdateRentalCommand: IRequest<bool>
    {
        public UpdateRentalCommand(int id, int carId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            CarId = carId;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
    }
}
