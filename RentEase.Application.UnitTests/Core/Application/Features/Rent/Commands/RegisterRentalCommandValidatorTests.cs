using RentEase.Application.Features.Rent.Commands;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Rent.Commands
{
    public class RegisterRentalCommandValidatorTests
    {
        private readonly RegisterRentalCommandValidator _validator;

        public RegisterRentalCommandValidatorTests()
        {
            _validator = new RegisterRentalCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_CarId_Is_NotGreatherThan0()
        {
            var command = new RegisterRentalCommand(DateTime.Now, DateTime.Now.AddDays(1), 0);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "CarId" && e.ErrorMessage == "{CarId} must be greater than 0.");
        }
        [Fact]
        public void Should_Have_Error_When_StartDate_Is_Null()
        {
            var command = new RegisterRentalCommand(null, DateTime.Now.AddDays(1), 1);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "StartDate" && e.ErrorMessage == "{StartDate} can't be null");
        }
        [Fact]
        public void Should_Have_Error_When_EndDate_Is_Null()
        {
            var command = new RegisterRentalCommand(DateTime.Now, null, 1);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "EndDate" && e.ErrorMessage == "{EndDate} can't be null");
        }
        [Fact]
        public void Should_Have_Error_When_StartDate_Is_Greater_Than_EndDate()
        {
            var command = new RegisterRentalCommand(DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), 1);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "StartDate" && e.ErrorMessage == "Start date must be less than or equal to end date.");
        }
        [Fact]
        public void Should_Be_Valid_When_All_Properties_Are_Correct()
        {
            var command = new RegisterRentalCommand(DateTime.Now, DateTime.Now.AddDays(1), 1);
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
