using SkyOdyssey.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto> GetLocationByIdAsync(int id);
    }
}
