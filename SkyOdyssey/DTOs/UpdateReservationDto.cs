namespace SkyOdyssey.DTOs
{
    public class UpdateReservationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
