using MediatR;

namespace RentEase.Application.Features.Car.Commands
{
    public class CreateCarCommand : IRequest<int>
    {
        public string Type { get; set; }
        public string Model { get; set; }
        public CreateCarCommand(string model, string type)
        {
            Model = model;
            Type = type;
        }
    }
}
