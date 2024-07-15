namespace Reservation.Application.DTOs.UserDTOs
{
    public class AuthResponseDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
