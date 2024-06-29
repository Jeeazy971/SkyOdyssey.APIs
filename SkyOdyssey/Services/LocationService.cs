using AutoMapper;
using SkyOdyssey.Models;
using SkyOdyssey.DTOs;

namespace SkyOdyssey.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto> GetLocationByIdAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            return _mapper.Map<LocationDto>(location);
        }

        public async Task CreateLocationAsync(Location location)
        {
            await _locationRepository.AddAsync(location);
        }

        public async Task<IEnumerable<LocationDto>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom = null, DateTime? availableTo = null, decimal? maxPrice = null, int? maxGuests = null)
        {
            var locations = await _locationRepository.SearchLocationsAsync(searchTerm, availableFrom, availableTo, maxPrice, maxGuests);
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }
    }
}
