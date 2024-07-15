using Reservation.Application.DTOs.Reservations;
using Reservation.Application.Features.RoomReservations.Queries.GetUserReservations;

namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateReservationDTO request)
        {
            var command = new CreateReservationCommand(request.RoomId,request.StartDate,request.EndDate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("Update")]
        
        public async Task<IActionResult> Update([FromBody] UpdateRoomReservationDTO request)
        {           
            var command = new UpdateRoomReservationCommand(request.Id,request.RoomId,request.StartDate,request.EndDate);
            var result = await _mediator.Send(command);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteReservationCommand(id);
            var result = await _mediator.Send(command);
            if (result)            
                return Ok();
            
            return NotFound();
        }

        [HttpGet("GetUserReservations")]
        public async Task<IActionResult> GetUserReservations()
        {           
            var command = new GetUserReservationQuery();
            var result = await _mediator.Send(command);            
                return Ok(result);
        }

    }
}
