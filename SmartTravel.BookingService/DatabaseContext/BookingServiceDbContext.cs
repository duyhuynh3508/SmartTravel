using Microsoft.EntityFrameworkCore;
using SmartTravel.Shared.Entities;

namespace SmartTravel.BookingService.DatabaseContext
{
    public class BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options) : DbContext(options)
    {
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<BookingStatusEnity> BookingStatuses { get; set; }
        public DbSet<CarRentalEntity> CarRentals { get; set; }
        public DbSet<CarTypeEntity> CarTypes { get; set; }
        public DbSet<FlightDetailEntity> FlightDetails { get; set; }
        public DbSet<HotelDetailEntity> HotelDetails { get; set; }
        public DbSet<RoomTypeEntity> RoomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingEntity>(entity =>
            {
                entity.ToTable("tblBookings");
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne<BookingStatusEnity>()
                .WithMany()
                .HasForeignKey(e => e.BookingStatusId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BookingStatusEnity>(entity =>
            {
                entity.ToTable("tblBookingStatuses");
                entity.HasKey(e => e.BookingStatusId);

                entity.Property(e => e.BookingStatusId)
                      .ValueGeneratedNever();
            });

            modelBuilder.Entity<CarRentalEntity>(entity =>
            {
                entity.ToTable("tblCarRentals");
                entity.HasKey(e => e.CarRentalId);

                entity.HasOne<BookingEntity>()
                .WithMany()
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<CarTypeEntity>()
                .WithMany()
                .HasForeignKey(e => e.CarTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CarTypeEntity>(entity =>
            {
                entity.ToTable("tblCarTypes");
                entity.HasKey(e => e.CarTypeId);

                entity.Property(e => e.CarTypeId)
                      .ValueGeneratedNever();
            });

            modelBuilder.Entity<FlightDetailEntity>(entity =>
            {
                entity.ToTable("tblFlightDetails");
                entity.HasKey(e => e.FlightDetailId);

                entity.HasOne<BookingEntity>()
                .WithMany()
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HotelDetailEntity>(entity =>
            {
                entity.ToTable("tblHotelDetails");
                entity.HasKey(e =>e.HotelDetailId);

                entity.HasOne<RoomTypeEntity>()
                .WithMany()
                .HasForeignKey(e => e.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<BookingEntity>()
                .WithMany()
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RoomTypeEntity>(entity =>
            {
                entity.ToTable("tblRoomTypes");
                entity.HasKey(e => e.RoomTypeId);

                entity.Property(e => e.RoomTypeId)
                      .ValueGeneratedNever();
            });
        }
    }
}
