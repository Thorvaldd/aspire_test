namespace Emp.Api.Queries;

public static class EmployeeQueries
{
    public static readonly string GetAllEmployees = 
        @"SELECT
            e.employee_id AS EmployeeId,
            e.salary AS Salary,
            e.start_job_date AS StartJobDate,
            p.first_name AS FirstName,
            p.last_name AS LastName,
            p.full_name AS FullName,
            d.name AS Department,
            dp.title AS Position,
            c.name as Company,
            c.company_id AS CompanyId
        FROM
            employee e
        INNER JOIN person p ON e.person_id = p.person_id
        INNER JOIN department d ON e.department_id = d.department_id
        INNER JOIN dict_position dp ON e.position_id = dp.position_id
        INNER JOIN company c ON e.company_id = c.company_id
        WHERE
            (@Name IS NULL OR p.first_name = @Name OR p.last_name = @Name)
            AND (@Position IS NULL OR dp.position_id = @Position)
            AND (@Department IS NULL OR d.department_id = @Department)";
}