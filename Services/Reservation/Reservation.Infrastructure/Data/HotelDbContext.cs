namespace Reservation.Infrastructure.Data
{
    public class HotelDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }
        public DbSet<RoomBlocking> RoomBlockings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<RoomReservation>().HasQueryFilter(r => !r.IsDeleted);
            base.OnModelCreating(modelBuilder);

            // Configure Room entity
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            // Configure Reservation entity
            modelBuilder.Entity<RoomReservation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.RoomId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                // Relationships
                entity.HasOne(e => e.Room)
                      .WithMany()
                      .HasForeignKey(e => e.RoomId);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
            });

            // Seed data
            modelBuilder.Entity<Room>().HasData(
             new Room { Id = 1 ,Name = "Room One", RoomType = RoomType.Single, Address = "Floor 6",  LocationURL = "https://BehindStairs" },
             new Room { Id = 2, Name = "Room Two", RoomType = RoomType.Double ,Address = "Floor 3", LocationURL = "https://BehindStairs" },
             new Room { Id = 3, Name = "Room Three", RoomType = RoomType.Quard, Address = "Floor 5", LocationURL = "https://BehindStairs" },
             new Room { Id = 4, Name = "Room Four", RoomType = RoomType.Triple, Address = "Floor 1", LocationURL  = "https://BehindStairs" }
         );
        }
    }
}
