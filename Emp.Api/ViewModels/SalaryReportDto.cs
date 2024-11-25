namespace Emp.Api.ViewModels;

public class SalaryReportDto
{
    public string Department { get; set; }
    public string Position { get; set; }
    public string FullName { get; set; }
    public decimal Salary { get; set; }
    public decimal TotalDepartmentSalary { get; set; }
}