﻿using System;

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
        public string Airline { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
