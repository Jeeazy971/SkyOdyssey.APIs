using System;
using System.Collections.Generic;

namespace SkyOdyssey.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        // Foreign key
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
