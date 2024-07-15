namespace Reservation.Application.DTOs.Reservations
{
    public class CreateReservationDTO
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
