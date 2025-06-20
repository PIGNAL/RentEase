using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.Application.Features.Car.Commands;
using RentEase.Application.Features.Car.Queries;
using RentEase.Application.Models;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<int>> CreateCar([FromBody] CreateCarCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<bool>> UpdateCar([FromBody] UpdateCarCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<bool>> DeleteCar(int id)
        {
            var command = new DeleteCarCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<CarDto>> GetCarById(int id)
        {
            var query = new GetCarQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCars()
        {
            var query = new GetAllCarsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("available")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetAvailableCars([FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var query = new GetAvailableCarsQuery(startDate, endDate);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
