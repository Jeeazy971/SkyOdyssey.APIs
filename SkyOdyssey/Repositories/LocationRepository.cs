using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Data;
using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }
    }
}
