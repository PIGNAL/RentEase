using FluentValidation;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalQueryValidator : AbstractValidator<GetRentalQuery>
    {
        public GetRentalQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}

