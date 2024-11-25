namespace Emp.Api.Queries;

public static class ReportingQueries
{
    public static readonly string GetSalaryReportByDepartment = 
        @"SELECT
            d.name AS Department,
            dp.title AS Position,
            p.full_name AS FullName,
            e.salary AS Salary,
            SUM(e.salary) OVER (PARTITION BY d.department_id) AS TotalDepartmentSalary
        FROM
            employee AS e
            INNER JOIN department d ON e.department_id = d.department_id
            INNER JOIN dict_position dp ON e.position_id = dp.position_id
            INNER JOIN person p ON e.person_id = p.person_id
        WHERE
            (@Department IS NULL OR d.name LIKE CONCAT('%', @Department, '%'))
            AND (@Position IS NULL OR dp.title LIKE CONCAT('%', @Position, '%'))
        ORDER BY d.name, dp.title";
}