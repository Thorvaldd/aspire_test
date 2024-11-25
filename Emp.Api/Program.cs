using Carter;
using Emp.Api.Endpoints.Employees;
using Emp.Api.ViewModels;
using Emp.EmployeesDb;
using Emp.GravatarAPI;
using Scalar.AspNetCore;

namespace Emp.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();
        
        // Add services to the container.
        builder.Services.AddCarter();
        builder.Services.AddSingleton<EmployeeContext>();
        builder.Services.AddCors();
        
        builder.AddMySqlDataSource("employees");
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        #region Add 3rd party integrations

        builder.Services.RegisterGravatarApi();

        #endregion

        var app = builder.Build();

        app.MapDefaultEndpoints();
        // Configure the HTTP request pipeline.
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(op =>
            {
                op.WithOpenApiRoutePattern("/openapi/v1.json");
            });
        }
        app.UseHttpsRedirection();
        app.UseCors(b =>
        {
            b
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
        });
        
        app.MapCarter();
        app.Run();
    }
}