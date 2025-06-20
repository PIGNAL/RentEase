using AutoMapper;
using MediatR;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Commands
{
    public class UpdateRentalCommandHandler :IRequestHandler<UpdateRentalCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRentalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            var rentalEntity = await _unitOfWork.Repository<Rental>()
                .GetAsync(r => r.CarId == request.CarId && r.Customer.Email == _currentUserService.Email, r => r.Car);
            if (rentalEntity == null)
            {
                throw new Exception("Rental not found.");
            }

            _mapper.Map(request, rentalEntity[0]);

            _unitOfWork.Repository<Rental>().UpdateEntity(rentalEntity[0]);
            var result = await _unitOfWork.Complete();
            if (result > 0)
            {
                await SendEmail(rentalEntity[0]);
                return true;
            }
            throw new Exception("Failed to update the Rental");
        }


        private async Task SendEmail(Rental rental)
        {
            var car = rental.Car ?? await _unitOfWork.Repository<Domain.Car>().GetByIdAsync(rental.CarId);

            var emailBody = $@"
                Dear {_currentUserService.FullName},

                Congratulations! Your car reservation has been successfully Updated.

                Reservation Details:
                - Car: {car?.Type ?? "N/A"} {car?.Model ?? ""}
                - Start Date: {rental.StartDate:yyyy-MM-dd}
                - End Date: {rental.EndDate:yyyy-MM-dd}

                Thank you for choosing RentEase!
            ";

            var email = new Email
            {
                To = _currentUserService.Email,
                Subject = "Car Reservation Update Confirmation",
                Body = emailBody
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
