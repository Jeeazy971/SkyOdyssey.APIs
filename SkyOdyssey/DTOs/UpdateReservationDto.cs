﻿namespace SkyOdyssey.DTOs
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public string Status { get; set; }

        public ICollection<LocationDto> Location { get; set; }
        public ICollection<FlightDto> Flights { get; set; }
    }
}
