using FluentValidation;

namespace RentEase.Application.Features.Rent.Commands
{
    public class CancelRentalCommandValidator : AbstractValidator<CancelRentalCommand>
    {
        public CancelRentalCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
