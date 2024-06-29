using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Models;
using SkyOdyssey.Data;

namespace SkyOdyssey.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, Flight flight)
        {
            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight == null) return false;

            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.DepartureAirport = flight.DepartureAirport;
            existingFlight.ArrivalAirport = flight.ArrivalAirport;
            existingFlight.DepartureTime = flight.DepartureTime;
            existingFlight.ArrivalTime = flight.ArrivalTime;
            existingFlight.Price = flight.Price;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
