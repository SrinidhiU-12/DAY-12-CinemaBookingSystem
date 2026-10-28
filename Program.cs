using System;
using System.Collections.Generic;
using DAY_12_CinemaBookingSystem.Services;
using DAY_12_CinemaBookingSystem.Repositories;

namespace DAY_12_CinemaBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var movieService = new MovieService();
            var bookingService = new BookingService();
            var auditRepo = new AuditLogRepository();

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("        CINEMA TICKET BOOKING SYSTEM              ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. View Active Movies");
                Console.WriteLine("2. View Upcoming Showtimes (Date & Time Filter)");
                Console.WriteLine("3. Book Tickets (ACID Transaction Workflow)");
                Console.WriteLine("4. View System Audit Logs");
                Console.WriteLine("5. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        movieService.DisplayActiveMovies();
                        break;

                    case "2":
                        movieService.DisplayActiveMovies();
                        Console.Write("\nEnter Movie ID to view upcoming showtimes: ");
                        if (int.TryParse(Console.ReadLine(), out int movieId))
                        {
                            bookingService.DisplayUpcomingShows(movieId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Movie ID format.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("\n--- BOOK TICKETS (TRANSACTION-BACKED) ---");
                        Console.Write("Enter User ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int userId)) { Console.WriteLine("Invalid User ID."); break; }

                        Console.Write("Enter Show ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int showId)) { Console.WriteLine("Invalid Show ID."); break; }

                        Console.Write("Enter Total Amount (₹): ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount)) { Console.WriteLine("Invalid Amount."); break; }

                        Console.Write("Enter Seat IDs separated by comma (e.g., 1,2,3): ");
                        string seatInput = Console.ReadLine();
                        var seatIds = new List<int>();
                        if (!string.IsNullOrWhiteSpace(seatInput))
                        {
                            foreach (var part in seatInput.Split(','))
                            {
                                if (int.TryParse(part.Trim(), out int sId))
                                    seatIds.Add(sId);
                            }
                        }

                        if (seatIds.Count == 0)
                        {
                            Console.WriteLine("No valid seats entered. Booking aborted.");
                            break;
                        }

                        bool success = bookingService.CompleteBooking(userId, showId, amount, seatIds);
                        if (success)
                        {
                            Console.WriteLine("\n✨ Booking Confirmed Successfully via ACID Transaction & Logged! ✨");
                        }
                        else
                        {
                            Console.WriteLine("\n❌ Booking Failed. Entire Transaction Rolled Back Safely.");
                        }
                        break;

                    case "4":
                        var logs = auditRepo.GetAllAuditLogs();
                        Console.WriteLine("\n========================================");
                        Console.WriteLine("        SYSTEM AUDIT LOGS               ");
                        Console.WriteLine("========================================");
                        if (logs.Count == 0)
                        {
                            Console.WriteLine("No audit logs recorded yet.");
                        }
                        else
                        {
                            foreach (var log in logs)
                            {
                                Console.WriteLine($"[{log.Timestamp:yyyy-MM-dd HH:mm:ss}] {log.ActionName}: {log.Details}");
                            }
                        }
                        Console.WriteLine("========================================");
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("\nThank you for using Cinema Booking System. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 5.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}