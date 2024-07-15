namespace Reservation.Application.Features.RoomReservations.Command.CreateReservation
{
    public class CreateReservationCommand : ICommand<CreateReservationResult>
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserId { get; set; }

        public CreateReservationCommand(int roomId, DateTime startDate, DateTime endDate, Guid userId = default)
        {
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
        }
    }
    public class CreateReservationResult
    {
        public int ReservationId { get; set; }
    }
}
