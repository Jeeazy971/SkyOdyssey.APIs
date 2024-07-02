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
            await _flightService.CreateFlightAsync(flightDto);
            return CreatedAtAction(nameof(GetFlightById), new { id = flightDto.Id }, flightDto);
        }

        [HttpGet("by-location/{locationId}")]
        public async Task<IActionResult> GetFlightsByLocationDestination(int locationId)
        {
            var flights = await _flightService.GetFlightsByLocationDestinationAsync(locationId);
            return Ok(flights);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> GetAvailableFlights()
        {
            var flights = await _flightService.GetAvailableFlightsAsync();
            return Ok(flights);
        }
    }
}
