namespace SkyOdyssey.DTOs
{
    public class CreateLocationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public int MaxGuests { get; set; }
        public bool IncludesTransport { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public IFormFile Image { get; set; } // Ajout de la propriété Image
    }
}
