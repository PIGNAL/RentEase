using FluentValidation;

namespace RentEase.Application.Features.Rent.Queries
{
    public class GetRentalByCustomerAndCarQueryValidator : AbstractValidator<GetRentalByCustomerAndCarQuery>
    {
        public GetRentalByCustomerAndCarQueryValidator()
        {
            RuleFor(r => r.CarId).NotNull().NotEmpty().WithMessage("Car ID is required.");
            RuleFor(r => r.CustomerId).NotNull().NotEmpty().WithMessage("Customer ID is required.");
        }
    }
}

