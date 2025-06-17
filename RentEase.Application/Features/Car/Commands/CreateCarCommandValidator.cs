using FluentValidation;

namespace RentEase.Application.Features.Car.Commands
{
    public class CreateCarCommandValidator: AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(d => d.Model).NotNull().WithMessage("{Model} can't be null");
            RuleFor(d => d.Type).NotNull().WithMessage("{Type} can't be null");
        }
    }
}
