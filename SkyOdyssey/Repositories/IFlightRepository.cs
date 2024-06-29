using SkyOdyssey.Models;

namespace SkyOdyssey.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight> GetByIdAsync(int id);
        Task AddAsync(Flight flight);
        Task<bool> UpdateAsync(int id, Flight flight);
        Task<bool> DeleteAsync(int id);
    }
}
