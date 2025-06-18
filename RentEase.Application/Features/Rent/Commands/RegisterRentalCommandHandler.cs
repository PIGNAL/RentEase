using AutoMapper;
using MediatR;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Domain;

namespace RentEase.Application.Features.Rent.Commands
{
    public class RegisterRentalCommandHandler : IRequestHandler<RegisterRentalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ICurrentUserService _currentUserService;

        public RegisterRentalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(RegisterRentalCommand request, CancellationToken cancellationToken)
        {
            var rentalEntity = _mapper.Map<Rental>(request);
            var customerId = await GetCustomerId();
            rentalEntity.CustomerId = customerId;

            _unitOfWork.Repository<Rental>().AddEntity(rentalEntity);
            var result = await _unitOfWork.Complete();

            if (result > 0)
            {
                await SendEmail(rentalEntity);
                return true;
            }

            throw new Exception("Failed to create a Rental");
        }

        private async Task SendEmail(Rental rental)
        {
            var car = rental.Car ?? await _unitOfWork.Repository<Domain.Car>().GetByIdAsync(rental.CarId);

            var emailBody = $@"
                Dear {_currentUserService.FullName},

                Congratulations! Your car reservation has been successfully created.

                Reservation Details:
                - Car: {car?.Type ?? "N/A"} {car?.Model ?? ""}
                - Start Date: {rental.StartDate:yyyy-MM-dd}
                - End Date: {rental.EndDate:yyyy-MM-dd}

                Thank you for choosing RentEase!
            ";

            var email = new Email
            {
                To = _currentUserService.Email,
                Subject = "Car Reservation Confirmation",
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

        private async Task<int> GetCustomerId()
        {
            var customerEntity = _mapper.Map<Customer>(_currentUserService);
            var customerExisting =
                await _unitOfWork.Repository<Customer>().GetAsync(c => c.Email == customerEntity.Email);
            if (customerExisting.Count == 0)
            {

                _unitOfWork.Repository<Customer>().AddEntity(customerEntity);
                await _unitOfWork.Complete();

                return customerEntity.Id;
            }

            return customerExisting.First().Id;
        }
    }
}
