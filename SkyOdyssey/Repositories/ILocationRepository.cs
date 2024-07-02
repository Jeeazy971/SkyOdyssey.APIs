using SkyOdyssey.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task<IEnumerable<Location>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<Location>> SearchAsync(string city, DateTime? availableFrom, DateTime? availableTo, decimal? price, int? maxGuests);
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(Location location);
        Task DeleteAsync(int id);
    }
}
