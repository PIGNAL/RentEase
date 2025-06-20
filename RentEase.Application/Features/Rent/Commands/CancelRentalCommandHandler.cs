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
            var rentalEntity = await _unitOfWork.Repository<Rental>().GetByIdAsync(request.Id);
            if (rentalEntity == null)
            {
                return false;
            }
            _unitOfWork.Repository<Rental>().DeleteEntity(rentalEntity);
            var result = await _unitOfWork.Complete();
            return result > 0;
        }
    }
}
