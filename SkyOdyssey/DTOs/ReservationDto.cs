﻿using SkyOdyssey.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
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