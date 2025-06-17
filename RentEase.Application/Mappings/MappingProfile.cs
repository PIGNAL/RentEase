using AutoMapper;
using RentEase.Application.Features.Car.Commands;
using RentEase.Domain;

namespace RentEase.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCarCommand, Car>();
        }
    }
}
