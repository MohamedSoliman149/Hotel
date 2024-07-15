namespace Reservation.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<IQueryable<Room>> GetRoomsAvailability(DateTime startDate, DateTime endDate, string searchKey, RoomType? roomType, string address, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Room> roomQuery = _context.Rooms.Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(searchKey))
            {
                roomQuery = roomQuery.Where(r => r.Name.ToLower().Contains(searchKey.ToLower()));
            }
            if (roomType.HasValue)
            {
                roomQuery = roomQuery.Where(r => r.RoomType == roomType.Value);
            }
            if (!string.IsNullOrEmpty(address))
            {
                roomQuery = roomQuery.Where(r => r.Address.ToLower().Contains(address.ToLower()));
            }

            roomQuery = roomQuery.Where(r => !r.RoomBlocking.Any(c => c.StartDate <= endDate && (c.EndDate >= startDate || c.EndDate == null)));

            return roomQuery;

        }

    }

}
