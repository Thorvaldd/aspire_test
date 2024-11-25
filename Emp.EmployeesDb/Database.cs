using Dapper;

namespace Emp.EmployeesDb;

public class Database
{
    private readonly EmployeeContext _context;
    
    private const string DbName = "employees";

    public Database(EmployeeContext context)
    {
        _context = context;
    }

    public async Task CreateDatabaseAsync(CancellationToken cancellationToken = default)
    {
        string sql = $"CREATE DATABASE IF NOT EXISTS {DbName};";
        
        var commandDef = new CommandDefinition(sql, cancellationToken: cancellationToken);

        using var connection = _context.CreateMasterConnection();
        
        await connection.ExecuteAsync(commandDef);
    }

    public async Task DeleteDatabaseAsync(CancellationToken cancellationToken = default)
    {
        string sql = $"DROP DATABASE {DbName};";
        
        var commandDef = new CommandDefinition(sql, cancellationToken: cancellationToken);
        
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(commandDef);
    }
}