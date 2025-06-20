using MediatR;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalsByUserQuery : IRequest<List<RentalDto>>
    {
    }
}
