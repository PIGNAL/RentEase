using MediatR;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Service.Queries
{
    public class GetCarServicesMaintenance : IRequest<List<CarServiceDto>>
    {

    }
}
