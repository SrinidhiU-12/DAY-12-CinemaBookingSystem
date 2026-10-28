namespace DAY_12_CinemaBookingSystem.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string Language { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } // Date & Time function focus
        public bool IsActive { get; set; }
    }
}