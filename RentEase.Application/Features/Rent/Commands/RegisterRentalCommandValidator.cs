using FluentValidation;

namespace RentEase.Application.Features.Rent.Commands
{
    public class RegisterRentalCommandValidator : AbstractValidator<RegisterRentalCommand>
    {
        public RegisterRentalCommandValidator()
        {
            RuleFor(d => d.CarId).GreaterThan(0).WithMessage("{CarId} must be greater than 0.");
            RuleFor(d => d.StartDate).NotNull().WithMessage("{StartDate} can't be null");
            RuleFor(d => d.EndDate).NotNull().WithMessage("{EndDate} can't be null");
            RuleFor(d => d.StartDate).LessThanOrEqualTo(d => d.EndDate)
                .WithMessage("Start date must be less than or equal to end date.");
        }
    }
}
