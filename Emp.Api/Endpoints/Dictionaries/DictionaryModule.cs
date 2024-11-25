using Carter;
using Dapper;
using Emp.Api.Queries;
using Emp.Api.ViewModels;
using Emp.EmployeesDb;
using Microsoft.AspNetCore.Mvc;

namespace Emp.Api.Endpoints.Dictionaries;

public class DictionaryModule : CarterModule
{
    public DictionaryModule()
    :base("/api/v1/dictionaries")
    {
        WithTags("Dictionaries")
            .IncludeInOpenApi();
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/positions", ([FromServices] EmployeeContext db) =>
        GetPositions(db))
        .Produces(StatusCodes.Status200OK)
        .Produces<IEnumerable<KeyValuePair<string, string>>>();

        #region Positions

        static async Task<IResult> GetPositions(EmployeeContext db)
        {
            IEnumerable<KeyValuePair<string, string>> response = await db.CreateConnection()
                .QueryAsync<KeyValuePair<string, string>>(DictionaryTableQueries.GetPositions);
            
            return Results.Ok(response);
        }
        
        #endregion

        app.MapGet("/company/{companyId}", (string companyId, EmployeeContext db) =>
                GetCompany(companyId, db))
            .Produces(StatusCodes.Status200OK)
            .Produces<CompanyDto>();
            
            
        
        #region Company

        static async Task<IResult> GetCompany(string companyId, EmployeeContext db)
        {
            if (!int.TryParse(companyId, out int parsedCompanyId))
            {
                return Results.BadRequest("Invalid company id");
            }
            
            string query = 
                @"SELECT 
                    company_id AS CompanyId,
                    name AS CompanyName,
                    company_logo AS CompanyLogo
                FROM 
                    company
                 WHERE company_id = @CompanyId";
            var queryParams = new
            {
                CompanyId = parsedCompanyId
            };
            var response = await db.CreateConnection()
                .QueryFirstOrDefaultAsync<CompanyDto>(query, queryParams);
            
            return Results.Ok(response);
        }
        #endregion
    }

}