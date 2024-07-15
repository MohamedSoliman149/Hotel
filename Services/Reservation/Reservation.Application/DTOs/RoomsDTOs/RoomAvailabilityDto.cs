namespace Reservation.Application.DTOs
{
    public class RoomAvailabilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBusy { get; set; }
        public RoomType RoomType { get; set; }
        public string Address { get; set; }
    }
}
