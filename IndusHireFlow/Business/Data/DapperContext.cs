using System.Data;
using Microsoft.Extensions.Configuration;

namespace Business.Data
{
    /// <summary>
    /// Provides SQL Server database connections for Dapper operations
    /// Manages connection lifecycle and configuration
    /// </summary>
    public class DapperContext
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly string _connectionString;   

        public DapperContext(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        /// <summary>
        /// Creates and returns a new open database connection
        /// </summary>
        /// <returns>Open IDbConnection to SQL Server</returns>
        public IDbConnection CreateConnection()
        {
            var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Gets the connection string for external use
        /// </summary>
        public string GetConnectionString() => _connectionString;
    }
}
