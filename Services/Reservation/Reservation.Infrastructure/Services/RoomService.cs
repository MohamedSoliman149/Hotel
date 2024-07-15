namespace Reservation.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public RoomService(IRoomRepository roomRepository, IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<PaginatedResult<RoomAvailabilityDto>> GetRoomsAvailability(GetRoomsAvailabilityQuery request)
        {
            var rooms = await _roomRepository.GetRoomsAvailability(
           request.StartDate, request.EndDate, request.SearchKey, request.RoomType,
           request.Address, request.PageNumber, request.PageSize);
            var count = rooms.LongCount();

            var roomReservations = await _reservationRepository.GetRoomsReservationIdsByDates(request.StartDate, request.EndDate);

            var data = rooms
           .Skip((request.PageNumber - 1) * request.PageSize)
           .Take(request.PageSize)
           .Select(room => new RoomAvailabilityDto
           {
               Id = room.Id,
               Name = room.Name,
               IsBusy = roomReservations.Any(rId => rId == room.Id),
               RoomType = room.RoomType,
               Address = room.Address
           }).OrderBy(a => a.Name);
            return new PaginatedResult<RoomAvailabilityDto>(request.PageNumber, request.PageSize, count, data);
        }


    }
}
