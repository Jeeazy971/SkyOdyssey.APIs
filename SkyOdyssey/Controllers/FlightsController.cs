using Microsoft.AspNetCore.Mvc;
using SkyOdyssey.Services;
using AutoMapper;
using SkyOdyssey.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyOdyssey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public FlightsController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDto flightDto)
        {
            var createdFlight = await _flightService.CreateFlightAsync(flightDto);
            return CreatedAtAction(nameof(GetFlightById), new { id = createdFlight.Id }, createdFlight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightDto flightDto)
        {
            var updated = await _flightService.UpdateFlightAsync(id, flightDto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var deleted = await _flightService.DeleteFlightAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
