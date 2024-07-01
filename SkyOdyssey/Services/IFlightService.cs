using SkyOdyssey.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
        Task<FlightDto> GetFlightByIdAsync(int id);
        Task CreateFlightAsync(FlightDto flightDto);
        Task<IEnumerable<FlightDto>> GetFlightsByLocationDestinationAsync(int locationId);
    }
}
