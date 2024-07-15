using BuildingBlocks.Behaviors;
using BuildingBlocks.CQRS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservation.Application.Features.Rooms.Queries.GetRoomsAvailability;
using System.Reflection;

namespace Reservation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                config.AddOpenBehavior(typeof(IQuery<>));
                config.AddOpenBehavior(typeof(IQueryHandler<,>));
                config.AddOpenBehavior(typeof(GetRoomsAvailabilityQueryHandler));
                config.AddOpenBehavior(typeof(GetRoomsAvailabilityQuery));

            });

            return services;
        }
    }

}
