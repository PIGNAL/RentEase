using FluentValidation;

namespace RentEase.Application.Features.Rent.Commands
{
    public class UpdateRentalCommandValidator : AbstractValidator<UpdateRentalCommand>
    {
        public UpdateRentalCommandValidator()
        {

            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be before end date.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after start date.");

            RuleFor(x => x.CarId)
                .GreaterThan(0)
                .WithMessage("CarId must be greater than 0.");
        }
    }
}
