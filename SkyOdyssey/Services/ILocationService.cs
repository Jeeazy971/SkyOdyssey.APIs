using System.Collections.Generic;
using System.Threading.Tasks;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;

namespace SkyOdyssey.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> GetLocationByIdAsync(int id);
        Task CreateLocationAsync(Location location);
        Task<IEnumerable<LocationDto>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests);
    }
}
