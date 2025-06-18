using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;

namespace RentEase.Application.Features.Car.Commands
{
    public class UpdateCarCommandHandler: IRequestHandler<UpdateCarCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = await _unitOfWork.Repository<Domain.Car>().GetByIdAsync(request.Id);
            if (carEntity == null)
            {
                throw new Exception("Car not found.");
            }
            _mapper.Map(request, carEntity);
            _unitOfWork.Repository<Domain.Car>().UpdateEntity(carEntity);
            var result = await _unitOfWork.Complete();
            return result > 0;
        }
    }
}
