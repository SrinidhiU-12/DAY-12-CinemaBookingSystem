using System;
using System.Collections.Generic;
using DAY_12_CinemaBookingSystem.Models;
using DAY_12_CinemaBookingSystem.Repositories;

namespace DAY_12_CinemaBookingSystem.Services
{
    public class BookingService
    {
        private readonly ShowtimeRepository _showtimeRepository = new ShowtimeRepository();
        private readonly BookingRepository _bookingRepository = new BookingRepository();

        public void DisplayUpcomingShows(int movieId)
        {
            var shows = _showtimeRepository.GetUpcomingShowsForMovie(movieId);
            Console.WriteLine("\n========================================");
            Console.WriteLine("        UPCOMING SHOWTIMES              ");
            Console.WriteLine("========================================");

            if (shows.Count == 0)
            {
                Console.WriteLine("No upcoming shows found for this movie.");
                return;
            }

            foreach (var show in shows)
            {
                Console.WriteLine($"Show ID: {show.ShowId} | Screen: {show.ScreenId} | Time: {show.ShowTime:yyyy-MM-dd HH:mm} | Price: ₹{show.TicketPrice}");
            }
            Console.WriteLine("========================================");
        }

        public bool CompleteBooking(int userId, int showId, decimal totalAmount, List<int> seatIds)
        {
            var booking = new Booking
            {
                UserId = userId,
                ShowId = showId,
                TotalAmount = totalAmount,
                Status = "Confirmed"
            };

            return _bookingRepository.CreateBooking(booking, seatIds);
        }
    }
}