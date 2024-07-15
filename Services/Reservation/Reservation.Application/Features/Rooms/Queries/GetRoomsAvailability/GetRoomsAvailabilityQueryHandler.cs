namespace Reservation.Application.Features.Rooms.Queries.GetRoomsAvailability
{
    public class GetRoomsAvailabilityQueryHandler : IQueryHandler<GetRoomsAvailabilityQuery, PaginatedResult<RoomAvailabilityDto>>
    {
        private readonly IRoomService _roomService;

        public GetRoomsAvailabilityQueryHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<PaginatedResult<RoomAvailabilityDto>> Handle(GetRoomsAvailabilityQuery request, CancellationToken cancellationToken)
        {
           return await _roomService.GetRoomsAvailability(request);
        }

    }
}
