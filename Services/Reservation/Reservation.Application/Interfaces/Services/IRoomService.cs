namespace Reservation.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<PaginatedResult<RoomAvailabilityDto>> GetRoomsAvailability(GetRoomsAvailabilityQuery request);
    }
}
