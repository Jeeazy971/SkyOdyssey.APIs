namespace SkyOdyssey.DTOs
{
    public class PaymentDto
    {
        public string Token { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "euro";  
        public int ReservationId { get; set; }
    }
}
