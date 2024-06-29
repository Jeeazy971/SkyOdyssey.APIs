namespace SkyOdyssey.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public UserDto User { get; set; }
        public LocationDto Location { get; set; }
    }
}
