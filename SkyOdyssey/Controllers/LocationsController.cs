using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.DTOs;
using SkyOdyssey.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
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
    }
}
