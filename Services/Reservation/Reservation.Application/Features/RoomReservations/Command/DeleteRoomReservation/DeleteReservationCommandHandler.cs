namespace Reservation.Application.Features.RoomReservations.Command.DeleteRoomReservation
{
    public class DeleteReservationCommandHandler : ICommandHandler<DeleteReservationCommand, bool>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id);
            if (reservation == null)
                throw new ReservationNotFoundException(request.Id);
            await _reservationRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
