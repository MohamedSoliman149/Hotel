namespace Reservation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrctureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetRoomsAvailabilityQuery).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetRoomsAvailabilityQueryHandler).Assembly));

            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddHttpContextAccessor();
            services.AddDbContext<HotelDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<HotelDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoomReservationService, RoomReservationService>();
            services.AddScoped<IRoomService, RoomService>();


            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            var configu = configuration["Jwt:Key"];
            var Issuer = configuration["Jwt:Issuer"];
            var Audience = configuration["Jwt:Audience"];

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Issuer,
                        ValidAudience = Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });           

            return services;
        }
    }
}
