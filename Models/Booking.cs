namespace DAY_12_CinemaBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now; // Timestamp tracking
        public string Status { get; set; } = "Confirmed";
    }
}