using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalQueryHandler: IRequestHandler<GetRentalQuery, RentalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRentalQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RentalDto> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            var rentalEntity = await _unitOfWork.Repository<Rental>()
                .GetAsync(r => r.Id == request.Id, r => r.Car);
            if (rentalEntity.Count == 0)
            {
                return null;
            }
            return _mapper.Map<RentalDto>(rentalEntity[0]);
        }
    }
}
