using RentEase.Application.Features.Car.Commands;
using Xunit;

namespace RentEase.UnitTests.Core.Application.Features.Car.Commands;

public class UpdateCarCommandValidatorTests
{
    private readonly UpdateCarCommandValidator _validator;
    public UpdateCarCommandValidatorTests()
    {
        _validator = new UpdateCarCommandValidator();
    }
    [Fact]
    public void Should_Have_Error_When_Model_Is_Null()
    {
        var command = new UpdateCarCommand(1, null, "Sedan");
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Model" && e.ErrorMessage == "Car model is required.");
    }
    [Fact]
    public void Should_Have_Error_When_Type_Is_Null()
    {
        var command = new UpdateCarCommand(1, "Toyota", null);
        var result = _validator.Validate(command);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Type" && e.ErrorMessage == "Car type is required.");
    }
    [Fact]
    public void Should_Be_Valid_When_All_Properties_Are_Correct()
    {
        var command = new UpdateCarCommand(1, "Toyota", "Sedan");
        var result = _validator.Validate(command);
        Assert.True(result.IsValid);
    }
}