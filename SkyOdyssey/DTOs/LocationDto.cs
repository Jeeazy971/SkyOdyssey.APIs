namespace SkyOdyssey.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public int MaxGuests { get; set; }
        public bool IncludesTransport { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string ImagePath { get; set; }
    }
}
