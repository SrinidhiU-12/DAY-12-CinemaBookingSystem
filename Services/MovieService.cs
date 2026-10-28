using System;
using System.Collections.Generic;
using DAY_12_CinemaBookingSystem.Models;
using DAY_12_CinemaBookingSystem.Repositories;

namespace DAY_12_CinemaBookingSystem.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository = new MovieRepository();

        public void DisplayActiveMovies()
        {
            var movies = _movieRepository.GetAllActiveMovies();
            Console.WriteLine("\n========================================");
            Console.WriteLine("        NOW SHOWING MOVIES              ");
            Console.WriteLine("========================================");

            if (movies.Count == 0)
            {
                Console.WriteLine("No active movies found.");
                return;
            }

            foreach (var movie in movies)
            {
                Console.WriteLine($"ID: {movie.MovieId} | Title: {movie.Title} | Genre: {movie.Genre} | Language: {movie.Language} | Release: {movie.ReleaseDate:yyyy-MM-dd}");
            }
            Console.WriteLine("========================================");
        }

        public void RegisterNewMovie(string title, string genre, int durationMinutes, string language, DateTime releaseDate)
        {
            var movie = new Movie
            {
                Title = title,
                Genre = genre,
                DurationMinutes = durationMinutes,
                Language = language,
                ReleaseDate = releaseDate,
                IsActive = true
            };

            _movieRepository.AddMovie(movie);
            Console.WriteLine($"Movie '{title}' successfully added to the system!");
        }
    }
}