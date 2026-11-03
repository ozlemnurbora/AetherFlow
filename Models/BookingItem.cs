namespace AetherFlow.Models
{
    public class BookingItem
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        public int DestinationId { get; set; }
        public string TourName { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }

        public decimal UnitPrice { get; set; }
        public int People { get; set; }
        public decimal LineTotal { get; set; }
    }
}
