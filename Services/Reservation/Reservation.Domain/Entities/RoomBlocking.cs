namespace Reservation.Domain.Entities
{
    public class RoomBlocking 
    {
        #region Constructors

        public RoomBlocking()
        {
        }
        public RoomBlocking(int roomId, DateTime startDate, DateTime? endDate, string reason)
        {
            SetRoomId(roomId);
            SetStartDate(startDate);
            SetEndDate(endDate);
            SetReason(reason);
        }
        #endregion

        #region Public Properties
        public int Id { get;  set; }

        public int RoomId { get; protected set; }
        public virtual Room Room { get; protected set; }

        public DateTime StartDate { get; protected set; }

        public DateTime? EndDate { get; protected set; }

        public string Reason { get; protected set; }
        #endregion

        #region Public Methods

        public void SetRoomId(int roomId)
        {
            RoomId = roomId;
        }
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
        public void SetEndDate(DateTime? endDate)
        {
            EndDate = endDate;
        }

        public void SetReason(string reason)
        {
            Reason = reason;
        }
        #endregion
    }

}
