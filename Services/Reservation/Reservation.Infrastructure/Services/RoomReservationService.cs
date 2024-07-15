
namespace Reservation.Infrastructure.Services
{
    public class RoomReservationService : IRoomReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoomReservationService(IReservationRepository reservationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _reservationRepository = reservationRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> UpdateReservation(UpdateRoomReservationCommand request)
        {

            request.UserId = GetUserId();

            if (HelperService.IsNotValidDateDuration(request.StartDate, request.EndDate))
            {
                throw new BusinessException("Invalid DateTime, Should be in the future & the same day with different Time Slots, Also Start Date Should be less than End Date");
            }
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);
            if (reservation == null)
                throw new ReservationNotFoundException(request.Id);
            if (reservation.UserId != request.UserId)
                throw new ReservationNotFoundException(request.Id);

            var checkRoomAvalabilty = await _reservationRepository.GetRoomsReservationIdsByDates(request.StartDate, request.EndDate);
            if (checkRoomAvalabilty.Contains(request.RoomId))
                throw new InternalServerException("This Room not available in this time.");
            reservation.StartDate = request.StartDate;
            reservation.EndDate = request.EndDate;
            reservation.RoomId = request.RoomId;
            reservation.UserId = request.UserId;
            await _reservationRepository.UpdateAsync(reservation);
            return true;
        }

        public Guid GetUserId()
        {
            var claims = _httpContextAccessor.HttpContext?.User?.Claims;

            // Log claims for debugging
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            var userIdClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                throw new Exception("User ID not found.");
            }

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new Exception("Invalid user ID format.");
            }

            return userId;
        }
        public async Task<IEnumerable<GetUserReservationResult>> GetUserReservations()
        {
            var userId = GetUserId();
            var reservatons =await _reservationRepository.GetByUserIdAsync(userId);
            return reservatons.Select(a => new GetUserReservationResult()
            {
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Name = a.Room.Name,
                Address = a.Room.Address,
                Id = a.RoomId,
                RoomType = a.Room.RoomType,
                IsUpcoming = a.StartDate >= DateTime.Now             
                
            });
        }
    }
}
