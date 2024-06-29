using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Models;

namespace SkyOdyssey.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Reservations)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Reservation)
                .WithMany(r => r.Flights)
                .HasForeignKey(f => f.ReservationId);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Reservation)
                .WithMany(r => r.Hotels)
                .HasForeignKey(h => h.ReservationId);
        }
    }
}
