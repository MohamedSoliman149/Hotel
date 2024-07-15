namespace Reservation.Application.Features.Rooms.Queries.GetRoomsAvailability
{
    public class GetAvalableRoomsQueryValidator : AbstractValidator<GetRoomsAvailabilityQuery>
    {
        public GetAvalableRoomsQueryValidator()
        {
            RuleFor(x => x.StartDate)
             .Must(BeAValidDate).WithMessage("Start date is required");
            RuleFor(x => x.SearchKey).NotNull().WithMessage("ttttt");
            RuleFor(x => x.EndDate).Must(BeAValidDate).WithMessage("End Date is required");


        }


        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
