using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkyOdyssey.Models;

namespace SkyOdyssey.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(int id);
        Task<IEnumerable<Location>> SearchAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests);
    }
}
