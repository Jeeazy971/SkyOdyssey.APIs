using SkyOdyssey.Models;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location> GetByIdAsync(int id);
    Task<IEnumerable<Location>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests);
    Task AddAsync(Location location);
}