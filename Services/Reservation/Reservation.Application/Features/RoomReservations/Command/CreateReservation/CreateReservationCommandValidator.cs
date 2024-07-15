namespace Reservation.Application.Features.RoomReservations.Command.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start Date is required");
            RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate is required");
            RuleFor(x => x.RoomId).NotEmpty().WithMessage("Room Id should not be empty");
        }
    }
}
