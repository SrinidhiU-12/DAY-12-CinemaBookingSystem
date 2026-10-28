using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAY_12_CinemaBookingSystem.Helpers;
using DAY_12_CinemaBookingSystem.Models;

namespace DAY_12_CinemaBookingSystem.Repositories
{
    public class BookingRepository
    {
        public bool CreateBooking(Booking booking, List<int> seatIds)
        {
            using (var connection = DatabaseHelper.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction()) // Start ACID Transaction
                {
                    try
                    {
                        // 1. Insert into Bookings table and retrieve the new BookingId using OUTPUT INSERTED
                        string bookingQuery = "INSERT INTO Bookings (UserId, ShowId, TotalAmount, BookingDate, Status) " +
                                              "OUTPUT INSERTED.BookingId " +
                                              "VALUES (@UserId, @ShowId, @TotalAmount, SYSDATETIME(), @Status);";

                        int bookingId;
                        using (var cmd = new SqlCommand(bookingQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserId", booking.UserId);
                            cmd.Parameters.AddWithValue("@ShowId", booking.ShowId);
                            cmd.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
                            cmd.Parameters.AddWithValue("@Status", booking.Status);

                            bookingId = (int)cmd.ExecuteScalar();
                        }

                        // 2. Insert associated seats into BookingSeats junction table
                        foreach (var seatId in seatIds)
                        {
                            string seatQuery = "INSERT INTO BookingSeats (BookingId, SeatId) VALUES (@BookingId, @SeatId);";
                            using (var cmd = new SqlCommand(seatQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                                cmd.Parameters.AddWithValue("@SeatId", seatId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 3. Record an entry in AuditLogs for tracking
                        string auditQuery = "INSERT INTO AuditLogs (ActionName, Details, Timestamp) VALUES (@ActionName, @Details, SYSDATETIME());";
                        using (var cmd = new SqlCommand(auditQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ActionName", "BOOKING_CREATED");
                            cmd.Parameters.AddWithValue("@Details", $"Successfully booked {seatIds.Count} seat(s) under Booking ID: {bookingId}");
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit(); // Commit all operations atomically
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback everything if an error occurs
                        Console.WriteLine($"Transaction Error (Rolled back): {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}