namespace Emp.EmployeesDb.Entities;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    
    /// <summary>
    /// gravatar api
    /// </summary>
    public string CompanyLogo { get; set; }
}