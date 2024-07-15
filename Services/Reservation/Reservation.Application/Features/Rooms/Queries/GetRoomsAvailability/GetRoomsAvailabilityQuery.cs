namespace Reservation.Application.Features.Rooms.Queries.GetRoomsAvailability
{
    public class GetRoomsAvailabilityQuery:IQuery<PaginatedResult<RoomAvailabilityDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? SearchKey { get; set; } = default!;
        public RoomType? RoomType { get; set; }
        public string? Address { get; set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
