
namespace Reservation.Application.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<RoomReservation>> GetByUserIdAsync(Guid userId);
        Task AddAsync(RoomReservation reservation);
        Task UpdateAsync(RoomReservation reservation);
        Task DeleteAsync(int Id);
        Task<RoomReservation> GetByIdAsync(int id);
        Task<List<int>> GetRoomsReservationIdsByDates(DateTime startDate, DateTime endDate);
    }
}
