using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;

namespace SkyOdyssey.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FlightDto>>(flights);
        }

        public async Task<FlightDto> GetFlightByIdAsync(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            return _mapper.Map<FlightDto>(flight);
        }

        public async Task<FlightDto> CreateFlightAsync(FlightDto flightDto)
        {
            var flight = _mapper.Map<Flight>(flightDto);
            await _flightRepository.AddAsync(flight);
            return _mapper.Map<FlightDto>(flight);
        }

        public async Task<bool> UpdateFlightAsync(int id, FlightDto flightDto)
        {
            var flight = _mapper.Map<Flight>(flightDto);
            return await _flightRepository.UpdateAsync(id, flight);
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            return await _flightRepository.DeleteAsync(id);
        }
    }
}
