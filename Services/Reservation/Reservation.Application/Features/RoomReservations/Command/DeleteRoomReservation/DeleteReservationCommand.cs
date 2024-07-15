namespace Reservation.Application.Features.RoomReservations.Command.DeleteRoomReservation
{
    public class DeleteReservationCommand:ICommand<bool>
    {
        public int Id { get; set; }
        public DeleteReservationCommand(int id)
        {
            Id = id;
        }
    }
}
