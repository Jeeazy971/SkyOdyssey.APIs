using SkyOdyssey.DTOs;

namespace SkyOdyssey.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
        Task<FlightDto> GetFlightByIdAsync(int id);
        Task<FlightDto> CreateFlightAsync(FlightDto flightDto);
        Task<bool> UpdateFlightAsync(int id, FlightDto flightDto);
        Task<bool> DeleteFlightAsync(int id);
    }
}
