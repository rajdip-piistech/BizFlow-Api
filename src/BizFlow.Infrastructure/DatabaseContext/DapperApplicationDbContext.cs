using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BizFlow.Infrastructure.DatabaseContext;

public class DapperApplicationDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperApplicationDbContext(IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        _configuration = configuration;
        // Safely get the connection string and handle potential null values
        _connectionString = _configuration.GetConnectionString(ApplicationConstants.DefaultConnection)
                            ?? throw new InvalidOperationException($"Connection string '{ApplicationConstants.DefaultConnection}' is not configured.");
    }

    public IDbConnection CreateConnection()
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("The connection string cannot be null or empty.");
        }
        return new SqlConnection(_connectionString);
    }
}