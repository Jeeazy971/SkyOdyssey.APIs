using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Data;
using SkyOdyssey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Location>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Locations.Where(l => ids.Contains(l.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Location>> SearchAsync(string city, DateTime? availableFrom, DateTime? availableTo, decimal? price, int? maxGuests)
        {
            var query = _context.Locations.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(l => l.City.Contains(city));
            }

            if (availableFrom.HasValue)
            {
                query = query.Where(l => l.AvailableFrom >= availableFrom.Value);
            }

            if (availableTo.HasValue)
            {
                query = query.Where(l => l.AvailableTo <= availableTo.Value);
            }

            if (price.HasValue)
            {
                query = query.Where(l => l.Price <= price.Value);
            }

            if (maxGuests.HasValue)
            {
                query = query.Where(l => l.MaxGuests >= maxGuests.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
