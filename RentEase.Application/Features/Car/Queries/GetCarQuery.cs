using MediatR;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetCarQuery : IRequest<CarDto>
    {
        public int Id { get; set; }

        public GetCarQuery(int id)
        {
            Id = id;
        }
    }
}