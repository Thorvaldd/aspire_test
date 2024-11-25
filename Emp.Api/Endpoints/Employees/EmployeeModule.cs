using Carter;
using Dapper;
using Emp.Api.Endpoints.Employees.RequestQueries;
using Emp.Api.Queries;
using Emp.Api.ViewModels;
using Emp.EmployeesDb;
using Microsoft.AspNetCore.Mvc;

namespace Emp.Api.Endpoints.Employees;

public class EmployeeModule : CarterModule
{
    public EmployeeModule()
    :base("/api/v1/employees")
    {
        WithTags("Employee");
        IncludeInOpenApi();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("", ([FromServices] EmployeeContext db, [AsParameters]EmployeeGetRequest request) =>
            GetEmployees(db, request))
            .Produces(StatusCodes.Status200OK)
            .Produces<IList<EmployeeDto>>();
        
        app.MapPut("/{employeeId}", (
                    [FromServices] EmployeeContext db, 
                    [FromBody]UpdateEmployeeDto request,
                    [FromRoute] string employeeId) =>
            UpdateEmployee(db, request, employeeId))
            .Produces(StatusCodes.Status200OK);

        #region GetEmployees

        static async Task<IResult> GetEmployees(EmployeeContext db, EmployeeGetRequest request)
        {
            int.TryParse(request.Position, out int parsedPosition);
            using var connection = db.CreateConnection();
            var response = await connection
                .QueryAsync<EmployeeDto>(EmployeeQueries.GetAllEmployees,
                    new
                    {
                        Position = parsedPosition >0 ? parsedPosition : (int?)null,
                        Name = string.IsNullOrEmpty(request.Name) ? null : request.Name,
                        Department = string.IsNullOrEmpty(request.Department) ? null : request.Department,
                    });
            
            return Results.Ok(response);
        }
        #endregion
        
        #region UpdateEmployee

        static async Task<IResult> UpdateEmployee(EmployeeContext db, UpdateEmployeeDto request, string employeeId)
        {
            var query = @"
                    UPDATE employee SET salary = @Salary WHERE employee_id = @EmployeeId;
                    
                    UPDATE person
                    SET first_name = @FirstName
                    WHERE person_id = (SELECT person_id FROM employee WHERE employee_id = @EmployeeId);";
            using var connection = db.CreateConnection();
            try
            {
                var queryParams = new
                {
                    request.Salary,
                    request.FirstName,
                    EmployeeId = long.Parse(employeeId)
                };
                
                await connection.ExecuteAsync(query, queryParams);
                return Results.Ok();
            }
            catch (Exception e)
            {
                // tr.Rollback();
                return Results.BadRequest(e.Message);
            }
        } 
        #endregion
    }
}