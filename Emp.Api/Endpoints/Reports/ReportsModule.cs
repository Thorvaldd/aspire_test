using System.Data;
using System.Text.Json;
using Carter;
using Dapper;
using Emp.Api.Queries;
using Emp.Api.ViewModels;
using Emp.EmployeesDb;
using Microsoft.AspNetCore.Mvc;

namespace Emp.Api.Endpoints.Reports;

public class ReportsModule : CarterModule
{
    public ReportsModule()
    :base("/api/v1/reports")
    {
        WithTags("Reports")
            .IncludeInOpenApi();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/salary",(
                    [FromServices] EmployeeContext db,
                    [FromBody] SalaryFilterDto postDto)
                => GenerateSalaryReport(db, postDto));


        static async Task<IResult> GenerateSalaryReport(EmployeeContext db, SalaryFilterDto postDto)
        {
            var queryParams = new
            {
                Department = string.IsNullOrWhiteSpace(postDto.Department) ? null : postDto.Department,
                Position = string.IsNullOrWhiteSpace(postDto.Position) ? null : postDto.Position,
            };

            Console.WriteLine($"Query params : {JsonSerializer.Serialize(queryParams)}");
            using IDbConnection connection = db.CreateConnection();
            IEnumerable<SalaryReportDto> result = await connection
                .QueryAsync<SalaryReportDto>(ReportingQueries.GetSalaryReportByDepartment, queryParams);
            
            return Results.Ok(result);
        }
    }
}