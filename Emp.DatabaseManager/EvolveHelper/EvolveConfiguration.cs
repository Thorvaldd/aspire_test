using System.Text.Json;
using EvolveDb;
using EvolveDb.Configuration;
using MySqlConnector;

namespace Emp.DatabaseManager.EvolveHelper;

public class EvolveConfiguration
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public EvolveConfiguration(ILogger logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public void StartMigration()
    {
        _logger.LogInformation("Logger config {Config}",_configuration.GetValue<string>("EvolveScriptsLocation"));
            CommandOptions command = CommandOptions.DoNothing;

            MySqlConnection connection = GetConnection();

            bool skipNextMigration = _configuration.GetValue<bool?>("SkipNextMigrations") ?? false;
            string? location = _configuration.GetValue<string?>("EvolveScriptsLocation");
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException("The location argument is missing.");
            }
            
            string evolveCommand = _configuration["EvolveCommand"];
            if(!string.IsNullOrEmpty(evolveCommand))
            {
                command = (CommandOptions)Enum.Parse(typeof(CommandOptions), evolveCommand, true);
            }

            var evolve = new Evolve(connection)
            {
                Locations = new List<string> { location },
                IsEraseDisabled = true,
                SkipNextMigrations = skipNextMigration,
                Command = command,
                OutOfOrder = true,
                CommandTimeout = 120
            };

            ExecuteCommand(command, evolve);
        
    }

    private static void ExecuteCommand(CommandOptions options, Evolve evolve)
    {
        switch (options)
        {
            case CommandOptions.Migrate:
                evolve.Repair();
                evolve.Migrate();
                break;
            case CommandOptions.Repair:
                evolve.Repair();
                break;
            case CommandOptions.Erase:
                evolve.Erase();
                break;
            case CommandOptions.Info:
                evolve.Info();
                break;
            case CommandOptions.Validate:
                evolve.Validate();
                break;
            case CommandOptions.DoNothing:
            default:
                break;

        }
    }
    private MySqlConnection GetConnection()
    {
        string baseConnectionString = _configuration.GetConnectionString("employees");
        
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(baseConnectionString)
        {
            AllowUserVariables = true,
        };

        return new MySqlConnection(builder.ToString());
    }
}