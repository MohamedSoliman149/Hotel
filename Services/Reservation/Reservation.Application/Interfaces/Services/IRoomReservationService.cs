namespace Reservation.Application.Interfaces.Services
{
    public interface IRoomReservationService
    {
        Task<bool> UpdateReservation(UpdateRoomReservationCommand request);
        Task<IEnumerable<GetUserReservationResult>> GetUserReservations();
    }
}
