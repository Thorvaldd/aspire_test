using System.Diagnostics;
using Emp.DatabaseManager.EvolveHelper;
using Emp.EmployeesDb;

namespace Emp.DatabaseManager;

public class EmployeeInitializer(IServiceProvider serviceProvider, ILogger<EmployeeInitializer> logger,
    IConfiguration config)
:BackgroundService
{
    public const string ActivitySourceName  = "Migrations";
    private readonly ActivitySource activitySource = new(ActivitySourceName);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
        var db = scope.ServiceProvider.GetRequiredService<Database>();
        
        using var activity = activitySource.StartActivity("Initializing employee database", ActivityKind.Client);
        
        await InitializeDatabaseAsync(dbContext, db, stoppingToken);
    }

    public async Task InitializeDatabaseAsync(EmployeeContext dbContext, Database db, CancellationToken stoppingToken)
    {
        var sw = new Stopwatch();
        
        await CreateDatabaseAsync(db, stoppingToken);
        await SeedAsync();
        
        logger.LogInformation("Database initialization completed after {ElapsedMisliseconds}ms", sw.ElapsedMilliseconds);
        
        sw.Stop();
    }

    private async Task CreateDatabaseAsync(Database db, CancellationToken stoppingToken)
    {
        await db.CreateDatabaseAsync(stoppingToken);
    }

    private async Task SeedAsync()
    {
        var evolve = new EvolveConfiguration(logger, config);

        evolve.StartMigration();
    }
}