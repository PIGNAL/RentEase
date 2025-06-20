using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalByCustomerAndCarQueryHandler: IRequestHandler<GetRentalByCustomerAndCarQuery, RentalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRentalByCustomerAndCarQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RentalDto> Handle(GetRentalByCustomerAndCarQuery request, CancellationToken cancellationToken)
        {
            var rentalEntity = await _unitOfWork.Repository<Rental>()
                .GetAsync(r => r.CarId == request.CarId && r.CustomerId == request.CustomerId, r => r.Car);
            if (rentalEntity == null)
            {
                throw new Exception("Rental not found.");
            }
            return _mapper.Map<RentalDto>(rentalEntity[0]);
        }
    }
}
