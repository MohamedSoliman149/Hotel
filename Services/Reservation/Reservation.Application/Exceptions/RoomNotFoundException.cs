namespace Reservation.Application.Exceptions
{
    public class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(int id) : base("Room", id)
        {
        }
    }
}
