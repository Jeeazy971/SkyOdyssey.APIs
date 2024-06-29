using SkyOdyssey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
    }
}
