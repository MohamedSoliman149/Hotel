namespace Reservation.Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommand:ICommand<AuthResponseDTO>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
