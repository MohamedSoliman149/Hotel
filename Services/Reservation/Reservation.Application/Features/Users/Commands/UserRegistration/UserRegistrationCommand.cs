namespace Reservation.Application.Features.Users.Commands
{
    public class UserRegistrationCommand : ICommand<AuthResponseDTO>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
