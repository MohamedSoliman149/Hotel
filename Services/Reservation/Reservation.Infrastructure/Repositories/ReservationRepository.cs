namespace Reservation.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDbContext _context;

        public ReservationRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomReservation>> GetByUserIdAsync(Guid userId)
        {
            return await _context.RoomReservations.Where(r => r.UserId == userId).Include(a=>a.Room).ToListAsync();
        }

        public async Task AddAsync(RoomReservation reservation)
        {
            await _context.RoomReservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomReservation reservation)
        {
            _context.RoomReservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.RoomReservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoomReservation> GetByIdAsync(int id)
        {
            return await _context.RoomReservations.FindAsync(id);
        }

        public async Task<List<int>> GetRoomsReservationIdsByDates(DateTime startDate,DateTime endDate)
        {
            return await _context.RoomReservations.Where(r => r.EndDate >= startDate && r.StartDate <= endDate).Select(r => r.RoomId).ToListAsync();
        }
    }
}
