using RentEase.Application.Features.Car.Commands;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands
{
    public class CreateCarCommandValidatorTests
    {
        private readonly CreateCarCommandValidator _validator;
        public CreateCarCommandValidatorTests()
        {
            _validator = new CreateCarCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Model_Is_Null()
        {
            var command = new CreateCarCommand(null, "Sedan");
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Model" && e.ErrorMessage == "{Model} can't be null");
        }

        [Fact]
        public void Should_Have_Error_When_Type_Is_Null()
        {
            var command = new CreateCarCommand("Toyota", null);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Type" && e.ErrorMessage == "{Type} can't be null");
        }

        [Fact]
        public void Should_Be_Valid_When_All_Properties_Are_Correct()
        {
            var command = new CreateCarCommand("Toyota", "Sedan");
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
