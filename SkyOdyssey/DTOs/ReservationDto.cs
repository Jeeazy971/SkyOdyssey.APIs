using System;
using System.Collections.Generic;

namespace SkyOdyssey.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }

        public ICollection<FlightDto> Flights { get; set; } = new List<FlightDto>();
        public ICollection<LocationDto> Locations { get; set; } = new List<LocationDto>();
    }
}
