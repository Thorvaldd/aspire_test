using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Emp.DatabaseManager;

internal class EmployeeInitializerHealthCheck(EmployeeInitializer employeeInitializer) : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var task = employeeInitializer.ExecuteTask;

        return task switch
        {
            { IsCompletedSuccessfully: true } => Task.FromResult(HealthCheckResult.Healthy()),
            { IsFaulted: true } => Task.FromResult(HealthCheckResult.Unhealthy(task.Exception?.InnerException?.Message,
                task.Exception)),
            { IsCanceled: true } => Task.FromResult(HealthCheckResult.Unhealthy("Db initializer is cancelled")),
            _ => Task.FromResult(HealthCheckResult.Degraded("Db initializer is in progress..."))
        };
    }
}