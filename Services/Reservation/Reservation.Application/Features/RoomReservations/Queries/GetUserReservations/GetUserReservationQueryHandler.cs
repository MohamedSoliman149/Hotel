using BuildingBlocks.CQRS;

namespace Reservation.Application.Features.RoomReservations.Queries.GetUserReservations
{
    public class GetUserReservationsQueryHandler : IQueryHandler<GetUserReservationQuery, IEnumerable<GetUserReservationResult>>
    {
        private readonly IRoomReservationService _roomReservationService;

        public GetUserReservationsQueryHandler(IRoomReservationService roomReservationService)
        {
            _roomReservationService = roomReservationService;
        }

        public async Task<IEnumerable<GetUserReservationResult>> Handle(GetUserReservationQuery request, CancellationToken cancellationToken)
        {
            return await _roomReservationService.GetUserReservations();
        }
    }
}
