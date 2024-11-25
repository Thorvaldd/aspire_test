using System.Text.Json;
using Emp.DatabaseManager;
using Emp.EmployeesDb;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddMySqlDataSource("employees");
builder.Services.AddSingleton<EmployeeContext>();
builder.Services.AddSingleton<Database>();


builder.Services.AddOpenTelemetry()
    .WithTracing(t => t.AddSource(EmployeeInitializer.ActivitySourceName));

builder.Services.AddSingleton<EmployeeInitializer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<EmployeeInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<EmployeeInitializerHealthCheck>("DbInitializer");

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.MapPost("/reset-db",
        async (Database db, EmployeeContext context, EmployeeInitializer employeeInitializer, CancellationToken cancellationToken) =>
        {
            await db.DeleteDatabaseAsync(cancellationToken);
            await employeeInitializer.InitializeDatabaseAsync(context, db, cancellationToken);
        });
}

app.MapDefaultEndpoints();

await app.RunAsync();