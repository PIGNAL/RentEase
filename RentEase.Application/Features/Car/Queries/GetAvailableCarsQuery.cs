using MediatR;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetAvailableCarsQuery:IRequest<List<CarDto>>
    {
        public GetAvailableCarsQuery(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
