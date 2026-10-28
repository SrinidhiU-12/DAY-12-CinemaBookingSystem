using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAY_12_CinemaBookingSystem.Helpers;
using DAY_12_CinemaBookingSystem.Models;

namespace DAY_12_CinemaBookingSystem.Repositories
{
    public class MovieRepository
    {
        public void AddMovie(Movie movie)
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                string query = "INSERT INTO Movies (Title, Genre, DurationMinutes, Language, ReleaseDate, IsActive) VALUES (@Title, @Genre, @DurationMinutes, @Language, @ReleaseDate, @IsActive)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Genre", movie.Genre);
                    command.Parameters.AddWithValue("@DurationMinutes", movie.DurationMinutes);
                    command.Parameters.AddWithValue("@Language", movie.Language);
                    command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate); // DateTime parameter
                    command.Parameters.AddWithValue("@IsActive", movie.IsActive);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Movie> GetAllActiveMovies()
        {
            var movies = new List<Movie>();
            using (var connection = DatabaseHelper.CreateConnection())
            {
                // Leveraging Date & Time condition to filter currently running/active movies
                string query = "SELECT MovieId, Title, Genre, DurationMinutes, Language, ReleaseDate, IsActive FROM Movies WHERE IsActive = 1";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            movies.Add(new Movie
                            {
                                MovieId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Genre = reader.GetString(2),
                                DurationMinutes = reader.GetInt32(3),
                                Language = reader.GetString(4),
                                ReleaseDate = reader.GetDateTime(5), // DateTime retrieval
                                IsActive = reader.GetBoolean(6)
                            });
                        }
                    }
                }
            }
            return movies;
        }
    }
}