using System;
using System.Collections.Generic;

namespace SkyOdyssey.DTOs
{
    public class UpdateReservationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }
        public List<FlightDto> Flights { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }
}
