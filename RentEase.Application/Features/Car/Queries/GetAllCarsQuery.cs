using MediatR;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetAllCarsQuery : IRequest<List<CarDto>>
    {
    }
}