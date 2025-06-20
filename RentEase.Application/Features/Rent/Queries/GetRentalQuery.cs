using RentEase.Application.Models;
using MediatR;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalQuery : IRequest<RentalDto>
    {
        public GetRentalQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
    }
}
