namespace Reservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(new { token = result.Token });
            }
            return BadRequest(new { errors = result.Errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(new { token = result.Token });
            }
            return BadRequest(new { errors = result.Errors });
        }
    }
}
