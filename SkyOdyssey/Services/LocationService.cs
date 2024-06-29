using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
