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

        // Foreign keys
        public int UserId { get; set; }
        public User User { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        // Navigation properties
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
