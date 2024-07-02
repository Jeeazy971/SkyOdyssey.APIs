using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight> GetByIdAsync(int id);
        Task<IEnumerable<Flight>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<Flight>> GetFlightsByLocationDestinationAsync(int locationId);
        Task AddAsync(Flight flight);
        Task UpdateAsync(Flight flight);
        Task DeleteAsync(Flight flight);
    }
}
