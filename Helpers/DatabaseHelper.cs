using Microsoft.Data.SqlClient;

namespace DAY_12_CinemaBookingSystem.Helpers
{
    public static class DatabaseHelper
    {
        // Update your connection string with your local SQL Server instance name
        private static readonly string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=CinemaBookingDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}