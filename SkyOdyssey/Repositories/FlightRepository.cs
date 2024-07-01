using SkyOdyssey.Data;
using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Flights.Include(f => f.Reservation).Include(f => f.Location).ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            return await _context.Flights.Include(f => f.Reservation).Include(f => f.Location).SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByLocationDestinationAsync(int locationId)
        {
            return await _context.Flights
                .Where(f => f.LocationId == locationId)
                .ToListAsync();
        }
    }
}
