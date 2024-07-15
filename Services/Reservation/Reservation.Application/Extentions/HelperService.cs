namespace Reservation.Application.Extentions
{
    public static class HelperService
    {
        public static bool IsNotValidDateDuration(DateTime startDate, DateTime endDate)
        {
            DateTime now = DateTime.UtcNow;
            return (startDate < now && endDate < now) || (startDate > endDate);
        }
    }
}
