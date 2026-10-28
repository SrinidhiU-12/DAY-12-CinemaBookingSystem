namespace DAY_12_CinemaBookingSystem.Models
{
    public class Showtime
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public int ScreenId { get; set; }
        public DateTime ShowTime { get; set; } // Critical Date & Time scheduling property
        public decimal TicketPrice { get; set; }
        public bool IsActive { get; set; }
    }
}