namespace DAY_12_CinemaBookingSystem.Models
{
    public class AuditLog
    {
        public int LogId { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}