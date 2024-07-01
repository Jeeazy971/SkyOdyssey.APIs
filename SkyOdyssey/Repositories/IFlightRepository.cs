using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<IEnumerable<Flight>> GetByIdsAsync(IEnumerable<int> ids);
        Task<Flight> GetByIdAsync(int id);
        Task AddAsync(Flight flight);
        Task<IEnumerable<Flight>> GetFlightsByLocationDestinationAsync(int locationId);
    }
}
