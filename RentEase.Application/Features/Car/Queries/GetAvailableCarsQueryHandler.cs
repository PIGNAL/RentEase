using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetAvailableCarsQueryHandler : IRequestHandler<GetAvailableCarsQuery, List<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAvailableCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CarDto>> Handle(GetAvailableCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _unitOfWork.CarRepository.GetAvailableCars(request.StartDate, request.EndDate);
            return _mapper.Map<List<CarDto>>(cars);
        }
    }
}
