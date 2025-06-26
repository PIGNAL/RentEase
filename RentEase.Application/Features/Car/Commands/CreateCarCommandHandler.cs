using AutoMapper;
using MediatR;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Persistence;

namespace RentEase.Application.Features.Car.Commands
{
    public class CreateCarCommandHandler: IRequestHandler<CreateCarCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICarMaintenanceService _carMaintenanceService;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICarMaintenanceService carMaintenanceService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carMaintenanceService = carMaintenanceService;
        }

        public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = _mapper.Map<Domain.Car>(request);

            _unitOfWork.Repository<Domain.Car>().AddEntity(carEntity);

            var result = await _unitOfWork.Complete();

            if (result > 0)
            {
                var from = DateTime.UtcNow;
                var to = from.AddMonths(2);
                await _carMaintenanceService.ScheduleServicesForCar(carEntity, from, to);

                return carEntity.Id;
            }

            throw new Exception("Failed to create car record.");
        }

    }
}
