using MediatR;

namespace RentEase.Application.Features.Car.Commands
{
    public class UpdateCarCommand: IRequest<bool>
    {
        public UpdateCarCommand(string model, int id, string type)
        {
            Model = model;
            Id = id;
            Type = type;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }
}
