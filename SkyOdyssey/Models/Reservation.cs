using System;
using System.Collections.Generic;

namespace SkyOdyssey.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Flight> Flights { get; set; } = new List<Flight>();
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}