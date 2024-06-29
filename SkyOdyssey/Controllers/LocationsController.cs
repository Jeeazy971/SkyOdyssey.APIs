using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.Services;
using AutoMapper;
using SkyOdyssey.DTOs;
using SkyOdyssey.Models;

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
        public async Task<IActionResult> CreateLocation([FromForm] LocationDto locationDto, [FromForm] IFormFile image)
        {
            if (image != null)
            {
                var imagePath = Path.Combine("uploads", $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}");
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                locationDto.ImagePath = imagePath;
            }

            var location = _mapper.Map<Location>(locationDto);
            await _locationService.CreateLocationAsync(location);
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> SearchLocations(string searchTerm, DateTime? availableFrom = null, DateTime? availableTo = null, decimal? maxPrice = null, int? maxGuests = null)
        {
            var locations = await _locationService.SearchLocationsAsync(searchTerm, availableFrom, availableTo, maxPrice, maxGuests);
            return Ok(locations);
        }
    }
}
