using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DAY_12_CinemaBookingSystem.Helpers;
using DAY_12_CinemaBookingSystem.Models;

namespace DAY_12_CinemaBookingSystem.Repositories
{
    public class AuditLogRepository
    {
        public List<AuditLog> GetAllAuditLogs()
        {
            var logs = new List<AuditLog>();
            using (var connection = DatabaseHelper.CreateConnection())
            {
                // Fetching audit entries ordered by timestamp (latest first)
                string query = "SELECT LogId, ActionName, Details, Timestamp FROM AuditLogs ORDER BY Timestamp DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new AuditLog
                            {
                                LogId = reader.GetInt32(0),
                                ActionName = reader.GetString(1),
                                Details = reader.GetString(2),
                                Timestamp = reader.GetDateTime(3) // DATETIME2 retrieval
                            });
                        }
                    }
                }
            }
            return logs;
        }
    }
}