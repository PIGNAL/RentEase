using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Queries
{
    public class GetCarQueryHandler: IRequestHandler<GetCarQuery, CarDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCarQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<CarDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            var carEntity = await _unitOfWork.Repository<Domain.Car>().GetByIdAsync(request.Id);
            
            if (carEntity == null)
            {
                throw new Exception("Car not found.");
            }

            return _mapper.Map<CarDto>(carEntity);
        }
    }
}
