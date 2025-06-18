using AutoMapper;
using MediatR;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;

namespace RentEase.Application.Features.Car.Commands
{
    public class CreateCarCommandHandler: IRequestHandler<CreateCarCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CreateCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = _mapper.Map<Domain.Car>(request);

            _unitOfWork.Repository<Domain.Car>().AddEntity(carEntity);

            var result = await _unitOfWork.Complete();

            if (result > 0)
            {
                await SendEmail(carEntity);
                return carEntity.Id;
            }
            else
            {
                throw new Exception("Failed to create car record.");
            }
        }

        private async Task SendEmail(Domain.Car car)
        {
            var email = new Email
            {
                To = "joniballatore@gmail.com",
                Body = "The Car has been successfully created.",
                Subject = "Car Created"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception e)
            {
               throw new Exception($"Failed to send email: {e.Message}", e);
            }
        }
    }
}
