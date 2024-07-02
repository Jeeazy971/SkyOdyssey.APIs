using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using SkyOdyssey.Repositories;

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

        public async Task CreateLocationAsync(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _locationRepository.AddAsync(location);
        }

        public async Task UpdateLocationAsync(int id, LocationDto locationDto)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                return;
            }

            _mapper.Map(locationDto, location);
            await _locationRepository.UpdateAsync(location);
        }

        public async Task DeleteLocationAsync(int id)
        {
            await _locationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LocationDto>> SearchLocationsAsync(string searchTerm, DateTime? availableFrom, DateTime? availableTo, decimal? maxPrice, int? maxGuests)
        {
            var locations = await _locationRepository.SearchAsync(searchTerm, availableFrom, availableTo, maxPrice, maxGuests);
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<IEnumerable<LocationDto>> GetAvailableLocationsAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }
    }
}
