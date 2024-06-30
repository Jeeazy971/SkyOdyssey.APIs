using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.Services;
using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null)
                return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromForm] CreateLocationRequest createLocationRequest)
        {
            if (createLocationRequest.Image != null)
            {
                var imagePath = Path.Combine("uploads", $"{Guid.NewGuid()}{Path.GetExtension(createLocationRequest.Image.FileName)}");
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await createLocationRequest.Image.CopyToAsync(stream);
                }
                createLocationRequest.ImagePath = imagePath;
            }

            var locationDto = _mapper.Map<LocationDto>(createLocationRequest);
            await _locationService.CreateLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = locationDto.Id }, locationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDto locationDto)
        {
            await _locationService.UpdateLocationAsync(id, locationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchLocations(string searchTerm, DateTime? availableFrom = null, DateTime? availableTo = null, decimal? maxPrice = null, int? maxGuests = null)
        {
            var locations = await _locationService.SearchLocationsAsync(searchTerm, availableFrom, availableTo, maxPrice, maxGuests);
            return Ok(locations);
        }
    }
}
