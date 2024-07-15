namespace Reservation.Domain.Entities
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User User { get; set; }
        public Room Room { get; set; } = default!;
        public bool IsDeleted { get; set; }
    }
}
