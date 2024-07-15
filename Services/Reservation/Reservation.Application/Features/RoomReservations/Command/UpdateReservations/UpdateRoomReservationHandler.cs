namespace Reservation.Application.Features.RoomReservations.Command.UpdateReservations
{
    public class UpdateRoomReservationHandler : ICommandHandler<UpdateRoomReservationCommand, bool>
    {
        private readonly IRoomReservationService _reservationService;

        public UpdateRoomReservationHandler(IRoomReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<bool> Handle(UpdateRoomReservationCommand request, CancellationToken cancellationToken)
        {
            return await _reservationService.UpdateReservation(request);
        }
    }
}
