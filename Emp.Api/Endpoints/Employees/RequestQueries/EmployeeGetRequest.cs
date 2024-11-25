namespace Emp.Api.Endpoints.Employees.RequestQueries;

public class EmployeeGetRequest
{
    public string? Name { get; set; } = null;
    public string? Position { get; set; }
    public string? Department { get; set; } = null;
}