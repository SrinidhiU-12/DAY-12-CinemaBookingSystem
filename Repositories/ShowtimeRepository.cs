using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAY_12_CinemaBookingSystem.Helpers;
using DAY_12_CinemaBookingSystem.Models;

namespace DAY_12_CinemaBookingSystem.Repositories
{
    public class ShowtimeRepository
    {
        public List<Showtime> GetUpcomingShowsForMovie(int movieId)
        {
            var shows = new List<Showtime>();
            using (var connection = DatabaseHelper.CreateConnection())
            {
                // Heavy reliance on Date & Time function: comparing ShowTime with current system time (SYSDATETIME)
                string query = "SELECT ShowId, MovieId, ScreenId, ShowTime, TicketPrice, IsActive " +
                               "FROM Shows WHERE MovieId = @MovieId AND ShowTime >= SYSDATETIME() AND IsActive = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MovieId", movieId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shows.Add(new Showtime
                            {
                                ShowId = reader.GetInt32(0),
                                MovieId = reader.GetInt32(1),
                                ScreenId = reader.GetInt32(2),
                                ShowTime = reader.GetDateTime(3), // DATETIME2 retrieval
                                TicketPrice = reader.GetDecimal(4),
                                IsActive = reader.GetBoolean(5)
                            });
                        }
                    }
                }
            }
            return shows;
        }
    }
}