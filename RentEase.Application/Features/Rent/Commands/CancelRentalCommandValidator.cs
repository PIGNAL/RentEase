using FluentValidation;

namespace RentEase.Application.Features.Rent.Commands
{
    public class CancelRentalCommandValidator : AbstractValidator<CancelRentalCommand>
    {
        public CancelRentalCommandValidator()
        {
            RuleFor(r => r.CarId).NotNull().NotEmpty().WithMessage("Car ID is required.");
            RuleFor(r => r.CustomerId).NotNull().NotEmpty().WithMessage("Customer ID is required.");
        }
    }
}
