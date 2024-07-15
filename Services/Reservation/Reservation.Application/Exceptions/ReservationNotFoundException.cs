namespace Reservation.Application.Exceptions
{
    public class ReservationNotFoundException : NotFoundException
    {
        public ReservationNotFoundException(int id) : base("RoomReservation", id)
        {
        }
    }
  
}
