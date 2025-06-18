using MediatR;

namespace RentEase.Application.Features.Car.Commands
{
    public class DeleteCarCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteCarCommand(int id)
        {
            Id = id;
        }
    }
}
