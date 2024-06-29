using SkyOdyssey.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto createReservationDto);
        Task<bool> UpdateReservationAsync(int id, UpdateReservationDto updateReservationDto);
        Task<bool> DeleteReservationAsync(int id);
    }
}
