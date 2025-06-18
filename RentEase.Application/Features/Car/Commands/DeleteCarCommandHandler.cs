using MediatR;
using RentEase.Application.Contracts.Persistence;

namespace RentEase.Application.Features.Car.Commands
{
    public class DeleteCarCommandHandler: IRequestHandler<DeleteCarCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCarCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = await _unitOfWork.Repository<Domain.Car>().GetByIdAsync(request.Id);
            if (carEntity == null)
            {
                return false;
            }
            _unitOfWork.Repository<Domain.Car>().DeleteEntity(carEntity);
            var result = await _unitOfWork.Complete();
            return result > 0; 
        }
    }
}
