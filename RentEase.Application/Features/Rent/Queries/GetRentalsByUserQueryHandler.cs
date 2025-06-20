using AutoMapper;
using MediatR;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalsByUserQueryHandler:IRequestHandler<GetRentalsByUserQuery, List<RentalDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetRentalsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<RentalDto>> Handle(GetRentalsByUserQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _unitOfWork.Repository<Rental>()
                .GetAsync(r => r.Customer.Email == _currentUserService.Email && !r.IsDeleted, r => r.Car);
            return _mapper.Map<List<RentalDto>>(rentals);
        }
    }
}
