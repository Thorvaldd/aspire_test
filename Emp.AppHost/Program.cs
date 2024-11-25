using Emp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var employeeDbName = "employees";

var mysql = builder.AddMySql("mysql")
    .WithEnvironment("MYSQL_DATABASE", employeeDbName)
    .WithBindMount("../Emp.Api/data/mysql", "/docker-entrypoint-initdb.d")
    .WithPhpMyAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var employeeDb =mysql.AddDatabase(employeeDbName); 

var employeeDbManger = builder.AddProject<Projects.Emp_DatabaseManager>("empployeedbmanager")
    .WaitFor(employeeDb)
    .WithReference(employeeDb)
    .WithHttpHealthCheck("/health")
    .WithHttpCommand("/reset-db", "Reset Database", iconName: "DatabaseLightning");

var employeeApi = builder.AddProject<Projects.Emp_Api>("employeeapi")
    .WaitFor(employeeDb)
    .WaitFor(employeeDbManger)
    .WithReference(employeeDb)
    .WithExternalHttpEndpoints();

builder.AddNpmApp("employee-portal","../employee-portal")
    .WithReference(employeeApi)
    .WaitFor(employeeApi)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();