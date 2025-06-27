using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentEase.Application.Features.Service.Queries;
using RentEase.Application.Models;

namespace RentEase.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("maintenance")]
        public async Task<ActionResult<IEnumerable<CarServiceDto>>> GetCarServicesMaintenance()
        {
            var query = new GetCarServicesMaintenance();
            var carServices = await _mediator.Send(query);
            return Ok(carServices);
        }
    }
}
