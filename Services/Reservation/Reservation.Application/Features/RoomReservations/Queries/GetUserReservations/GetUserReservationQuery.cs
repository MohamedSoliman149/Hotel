namespace Reservation.Application.Features.RoomReservations.Queries.GetUserReservations
{
    public class GetUserReservationQuery :IQuery<IEnumerable<GetUserReservationResult>>
    {
       
    }

    public class GetUserReservationResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUpcoming { get; set; }
        public RoomType RoomType { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
