using AutoMapper;
using RentEase.Application.Contracts;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.Features.Rent.Commands;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCarCommand, Car>();
            CreateMap<UpdateCarCommand, Car>();
            CreateMap<Car, CarDto>();
            CreateMap<RegisterRentalCommand, Rental>();
            CreateMap<ICurrentUserService, Customer>();
        }
    }
}