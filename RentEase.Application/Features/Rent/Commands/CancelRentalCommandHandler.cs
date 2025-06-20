using MediatR;
using RentEase.Application.Contracts.Persistence;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Commands
{
    public class CancelRentalCommandHandler : IRequestHandler<CancelRentalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelRentalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelRentalCommand request, CancellationToken cancellationToken)
        {
            var rentalEntity = await _unitOfWork.Repository<Rental>()
                .GetAsync(r => r.CarId == request.CarId && r.CustomerId == request.CustomerId, r => r.Car);
            if (rentalEntity == null)
            {
                throw new Exception("Rental not found.");
            }
            _unitOfWork.Repository<Rental>().DeleteEntity(rentalEntity[0]);
            var result = await _unitOfWork.Complete();
            return result > 0;
        }
    }
}
