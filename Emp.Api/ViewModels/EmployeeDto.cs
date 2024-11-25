namespace Emp.Api.ViewModels;

public class EmployeeDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string Company { get; set; }
    public decimal Salary { get; set; }
    public DateTime StartJobDate { get; set; }
    public int CompanyId { get; set; }
}