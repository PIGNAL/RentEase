using FluentValidation;

namespace RentEase.Application.Features.Car.Commands
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Car ID is required.");
            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Car model is required.")
                .MaximumLength(100).WithMessage("Car model cannot exceed 100 characters.");
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Car type is required.")
                .MaximumLength(50).WithMessage("Car type cannot exceed 50 characters.");
        }
    }
}
