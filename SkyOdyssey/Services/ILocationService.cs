using SkyOdyssey.DTOs;

namespace SkyOdyssey.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> GetLocationByIdAsync(int id);
        Task CreateLocationAsync(LocationDto locationDto);
        Task UpdateLocationAsync(int id, LocationDto locationDto);
        Task DeleteLocationAsync(int id);
        Task<IEnumerable<LocationDto>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests);
        Task<IEnumerable<LocationDto>> GetAvailableLocationsAsync();
    }
}
