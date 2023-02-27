namespace BookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int BookedQunatity { get; set; }
        public Resource Resource { get; set; }

        public int ResourceId { get; set; }

    }
}
