using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task AddAsync(Location location);
    }
}
