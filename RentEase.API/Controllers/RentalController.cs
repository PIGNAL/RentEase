using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.Application.Features.Rent.Commands;
using RentEase.Application.Features.Rent.Queries;
using RentEase.Application.Models;

namespace RentEase.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RegisterRental([FromBody] RegisterRentalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateRental([FromBody] UpdateRentalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> CancelRental(int id)
        {
            var command = new CancelRentalCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            var query = new GetRentalsByUserQuery();
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RentalDto>> GetRentalByCustomerAndCar(int id)
        {
            var query = new GetRentalQuery(id);
            var rental = await _mediator.Send(query);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

    }
}
