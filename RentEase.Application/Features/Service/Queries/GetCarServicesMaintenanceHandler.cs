using AutoMapper;
using MediatR;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Service.Queries;

public class GetCarServicesMaintenanceHandler : IRequestHandler<GetCarServicesMaintenance, List<CarServiceDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetCarServicesMaintenanceHandler(ICarMaintenanceService carMaintenanceService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<CarServiceDto>> Handle(GetCarServicesMaintenance request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        var twoWeeks = today.AddDays(14);

        var services = await _unitOfWork.Repository<Domain.Service>()
            .GetAsync(
                s => s.Date >= today && s.Date <= twoWeeks,
                s => s.Car!
            );
        return _mapper.Map<List<CarServiceDto>>(
            services.OrderBy(s => s.Date)
        );
    }
}