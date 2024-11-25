DROP PROCEDURE IF EXISTS AddUserToCompany;

CREATE PROCEDURE add_person_to_company(
    IN personId INT,         --  person ID
    IN companyId INT,        --  company ID
    IN departmentId INT,     --  department ID
    IN positionId INT,       --  position ID
    IN startJobDate DATE,    -- Job start date
    IN salary DECIMAL(10,2)  --  salary
)
BEGIN
    INSERT INTO employee (person_id, company_id, department_id, position_id, start_job_date, salary)
    VALUES (personId, companyId, departmentId, positionId, startJobDate, salary);
END
