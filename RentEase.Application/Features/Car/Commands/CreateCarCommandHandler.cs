using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Persistence;

namespace RentEase.Application.Features.Car.Commands
{
    public class CreateCarCommandHandler: IRequestHandler<CreateCarCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = _mapper.Map<Domain.Car>(request);

            _unitOfWork.Repository<Domain.Car>().AddEntity(carEntity);

            var result = await _unitOfWork.Complete();

            if (result > 0)
            {
                return carEntity.Id;
            }

            throw new Exception("Failed to create car record.");
        }

    }
}
