using Bogus;
using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SkyOdyssey.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Reservations)
                .WithOne(r => r.Location)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Flights)
                .WithOne(f => f.Location)
                .HasForeignKey(f => f.LocationId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Flights)
                .WithOne(f => f.Reservation)
                .HasForeignKey(f => f.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Hotels)
                .WithOne(h => h.Reservation)
                .HasForeignKey(h => h.ReservationId);
        }

        public static void SeedData(ApplicationDbContext context)
        {
            if (context.Users.Any() || context.Locations.Any() || context.Flights.Any() || context.Reservations.Any() || context.Hotels.Any())
            {
                return;
            }

            var userIds = 1;
            var userFaker = new Faker<User>("fr")
                .RuleFor(u => u.Id, f => userIds++)
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, (f, u) =>
                {
                    byte[] passwordHash, passwordSalt;
                    PasswordHelper.CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    return passwordHash;
                })
                .RuleFor(u => u.PasswordSalt, (f, u) =>
                {
                    byte[] passwordHash, passwordSalt;
                    PasswordHelper.CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    return passwordSalt;
                });

            var users = userFaker.Generate(10);

            var locationIds = 1;
            var locationFaker = new Faker<Location>("fr")
                .RuleFor(l => l.Id, f => locationIds++)
                .RuleFor(l => l.City, f => f.Address.City())
                .RuleFor(l => l.Name, (f, l) => $"{l.City} Hotel")
                .RuleFor(l => l.Description, (f, l) =>
                    $"Profitez de notre hôtel {l.Name} situé au cœur de {l.City}. Nos chambres offrent un confort exceptionnel avec des équipements modernes, y compris Wi-Fi gratuit, télévision à écran plat, minibar, et plus encore. Détendez-vous dans notre spa ou profitez de notre salle de sport entièrement équipée. Idéalement situé près des attractions touristiques locales et des centres d'affaires.")
                .RuleFor(l => l.AvailableFrom, f => f.Date.Past())
                .RuleFor(l => l.AvailableTo, f => f.Date.Future())
                .RuleFor(l => l.MaxGuests, f => f.Random.Int(1, 10))
                .RuleFor(l => l.IncludesTransport, f => f.Random.Bool())
                .RuleFor(l => l.Price, f => Math.Round(f.Random.Decimal(50, 3000), 2))
                .RuleFor(l => l.ImagePath, f => $"uploads/{f.Random.Guid()}.jpg");

            var locations = locationFaker.Generate(100);

            var reservationIds = 1;
            var reservationFaker = new Faker<Reservation>("fr")
                .RuleFor(r => r.Id, f => reservationIds++)
                .RuleFor(r => r.StartDate, f => f.Date.Past())
                .RuleFor(r => r.EndDate, f => f.Date.Future())
                .RuleFor(r => r.NumberOfGuests, f => f.Random.Int(1, 10))
                .RuleFor(r => r.TotalPrice, f => Math.Round(f.Random.Decimal(100, 1500), 2))
                .RuleFor(r => r.UserId, f => f.PickRandom(users).Id)
                .RuleFor(r => r.LocationId, f => f.PickRandom(locations).Id)
                .RuleFor(r => r.Status, f => "Pending");

            var reservations = reservationFaker.Generate(200);

            var airlines = new[] { "Air France", "British Airways", "Lufthansa", "Emirates", "Qatar Airways", "Delta Airlines", "American Airlines", "KLM", "Singapore Airlines", "Turkish Airlines" };

            var flightIds = 1;
            var flightFaker = new Faker<Flight>("fr")
                .RuleFor(f => f.Id, f => flightIds++)
                .RuleFor(f => f.FlightNumber, f => f.Random.String2(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"))
                .RuleFor(f => f.DepartureAirport, f => $"{f.Address.City()} International Airport")
                .RuleFor(f => f.ArrivalAirport, f => $"{f.Address.City()} International Airport")
                .RuleFor(f => f.DepartureTime, f => f.Date.Future())
                .RuleFor(f => f.ArrivalTime, f => f.Date.Future())
                .RuleFor(f => f.Price, f => Math.Round(f.Random.Decimal(100, 1200), 2))
                .RuleFor(f => f.Airline, f => f.PickRandom(airlines))
                .RuleFor(f => f.ReservationId, f => f.PickRandom(reservations).Id)
                .RuleFor(f => f.LocationId, f => f.PickRandom(locations).Id);

            var flights = flightFaker.Generate(200);

            context.Users.AddRange(users);
            context.Locations.AddRange(locations);
            context.Reservations.AddRange(reservations);
            context.Flights.AddRange(flights);
            context.SaveChanges();
        }
    }

    public static class PasswordHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
