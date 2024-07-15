namespace Reservation.Application.Features.RoomReservations.Command.CreateReservation
{
    public class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, CreateReservationResult>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateReservationCommandHandler(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<CreateReservationResult> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (HelperService.IsNotValidDateDuration(request.StartDate, request.EndDate))
            {
                throw new BusinessException("Invalid DateTime, Should be in the future & the same day with different Time Slots, Also Start Date Should be less than End Date");
            }
            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new RoomNotFoundException(request.RoomId);

            var reservation = new RoomReservation
            {
                UserId = request.UserId,
                RoomId = request.RoomId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            await _reservationRepository.AddAsync(reservation);
            return new CreateReservationResult() { ReservationId = reservation.Id };

        }
    }
}
