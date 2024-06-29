using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Data;
using SkyOdyssey.Models;

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

    public async Task<IEnumerable<Location>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests)
    {
        var query = _context.Locations.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(l => l.Name.Contains(searchTerm) || l.City.Contains(searchTerm));
        }

        if (availableFrom.HasValue)
        {
            query = query.Where(l => l.AvailableFrom >= availableFrom.Value);
        }

        if (availableTo.HasValue)
        {
            query = query.Where(l => l.AvailableTo <= availableTo.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(l => l.Price <= maxPrice.Value);
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
}