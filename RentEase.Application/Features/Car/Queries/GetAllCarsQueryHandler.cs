using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, List<CarDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCarsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var carEntities = _unitOfWork.Repository<Domain.Car>().GetAsync(c => !c.IsDeleted);
            return carEntities.ContinueWith(task => _mapper.Map<List<CarDto>>(task.Result), cancellationToken);
        }
    }
}