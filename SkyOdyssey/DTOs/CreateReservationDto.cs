namespace SkyOdyssey.DTOs
{
    public class CreateReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public ICollection<FlightDto> Flights { get; set; }
        public ICollection<HotelDto> Hotels { get; set; }
    }
}
