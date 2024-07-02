using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Data;
using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Flight>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Flights.Where(f => ids.Contains(f.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByLocationDestinationAsync(int locationId)
        {
            return await _context.Flights.Where(f => f.LocationId == locationId).ToListAsync();
        }

        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flight flight)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }
}
