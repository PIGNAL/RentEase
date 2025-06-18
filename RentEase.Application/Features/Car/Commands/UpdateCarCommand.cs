using MediatR;

namespace RentEase.Application.Features.Car.Commands
{
    public class UpdateCarCommand : IRequest<bool>
    {
        public UpdateCarCommand(int id, string model, string type)
        {
            Id = id;
            Model = model;
            Type = type;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }
}
