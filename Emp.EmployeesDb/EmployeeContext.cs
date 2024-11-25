using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Emp.EmployeesDb;

public class EmployeeContext
{
    private readonly IConfiguration _configuration;
    public EmployeeContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_configuration.GetConnectionString("employees"));
    }

    public IDbConnection CreateMasterConnection()
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(_configuration.GetConnectionString("employees"))
        {
            Database = null
        };
        
        return new MySqlConnection(connectionStringBuilder.ToString());
    }
        
}