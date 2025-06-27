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
            CreateMap<UpdateRentalCommand, Rental>();
            CreateMap<RegisterRentalCommand, Rental>();
            CreateMap<Rental, RentalDto>();
            CreateMap<ICurrentUserService, Customer>();
            CreateMap<Service, CarServiceDto>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Car!.Model))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Car!.Type))
                .ForMember(dest => dest.ServiceDate, opt => opt.MapFrom(src => src.Date));


        }
    }
}