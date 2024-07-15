namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetRoomsAvailability")]
        public async Task<IActionResult> GetRoomsAvailability([FromQuery] GetRoomsAvailabilityQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
