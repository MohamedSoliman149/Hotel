namespace Reservation.Application.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task<IQueryable<Room>> GetRoomsAvailability(DateTime startDate, DateTime endDate, string searchKey, RoomType? roomType,
            string address, int? pageNumber = null, int? pageSize = null);
    }
}
