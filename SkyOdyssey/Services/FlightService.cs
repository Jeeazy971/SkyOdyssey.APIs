using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

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

        public async Task CreateFlightAsync(FlightDto flightDto)
        {
            var flight = _mapper.Map<Flight>(flightDto);
            await _flightRepository.AddAsync(flight);
        }

        public async Task<IEnumerable<FlightDto>> GetFlightsByLocationDestinationAsync(int locationId)
        {
            var flights = await _flightRepository.GetFlightsByLocationDestinationAsync(locationId);
            return _mapper.Map<IEnumerable<FlightDto>>(flights);
        }
    }
}
