using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.Application.Features.Rent.Commands;

namespace RentEase.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> RegisterRental([FromBody] RegisterRentalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
