namespace Reservation.Application.Features.RoomReservations.Command.UpdateReservations
{
    public class UpdateRoomReservationCommand : ICommand<bool>
    {
        public UpdateRoomReservationCommand(int id, int roomId, DateTime startDate, DateTime endDate, Guid userId = default)
        {
            Id = id;
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }

    }
}
