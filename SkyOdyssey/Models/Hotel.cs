namespace SkyOdyssey.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
