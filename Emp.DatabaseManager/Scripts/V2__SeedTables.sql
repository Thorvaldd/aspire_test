INSERT IGNORE INTO dict_position(title) VALUES
                                            ('Software Engineer'),
                                            ('HR Manager'),
                                            ('Lead generator');

SET @software_engineer_position_id = (SELECT position_id FROM dict_position WHERE title = 'Software Engineer');
SET @hr_manager_position_id = (SELECT position_id FROM dict_position WHERE title = 'HR Manager');
SET @lead_generator_position_id = (SELECT position_id FROM dict_position WHERE title = 'Lead generator');


INSERT IGNORE INTO dict_address_type(name)
VALUES('Company'), ('Employee');

SET @company_address_type_id = (SELECT address_type_id FROM dict_address_type WHERE name = 'Company');
SET @employee_address_type_id = (SELECT address_type_id FROM dict_address_type WHERE name = 'Employee');




INSERT IGNORE INTO address (line_1, line_2, city, state, postal_code, country, address_type_id)
VALUES
    ('Corporate Avenu 20', NULL, 'Kyiv', NULL, '1001','Ukraine', @company_address_type_id),
    ('Khreshatuk street 22', NULL,'Kyiv',NULL,'01001','Ukraine', @company_address_type_id),
    ('Employee street 20', 'Apt 101', 'Dnipro', NULL, 9001, 'Ukraine', @employee_address_type_id ),
    ('Employee street 16', NULL, 'Ivano-Frankifsk', NULL, 3031, 'Ukraine', @employee_address_type_id);

SET @company_address_1_id = (SELECT address_id FROM address WHERE line_1 = 'Corporate Avenu 20');
SET @company_address_2_id = (SELECT address_id FROM address WHERE line_1 = 'Khreshatuk street 22');
SET @employee_address_1_id = (SELECT address_id FROM address WHERE line_1 = 'Employee street 20');
SET @employee_address_2_id = (SELECT address_id FROM address WHERE line_1 = 'Employee street 16');



INSERT INTO company(name, company_logo)
VALUES
    ('UPost', 'u_post.png'),
    ('Volia', 'volia.png');

SET @u_post_id = (SELECT company_id FROM company WHERE name = 'UPost');
SET @volia_id = (SELECT company_id FROM company WHERE name = 'Volia');



INSERT INTO department(name, company_id)
VALUES
    ('Engineering', @u_post_id),
    ('Human Resources', @volia_id);

SET @engineering_dept_id = (SELECT department_id FROM department WHERE name = 'Engineering');
SET @hr_dept_id = (SELECT department_id FROM department WHERE name = 'Human Resources');


INSERT INTO person(first_name, last_name, dob)
VALUES
    ('John', 'Doe', '1990-01-15'),
    ('Jane', 'Smith', '1985-06-19'),
    ('Emily', 'Johnson', '1992-03-10');

SET @john_doe_id = (SELECT person_id FROM person WHERE first_name = 'John' AND last_name = 'Doe');
SET @jane_smith_id = (SELECT person_id FROM person WHERE first_name = 'Jane' AND last_name = 'Smith');
SET @emily_johnson_id = (SELECT person_id FROM person WHERE first_name = 'Emily' AND last_name = 'Johnson');



INSERT INTO employee (department_id, position_id, company_id, person_id, start_job_date, salary)
VALUES
    (@engineering_dept_id, @software_engineer_position_id, @u_post_id,
     @john_doe_id, '2022-01-01', 85000.00),
    
    (@hr_dept_id, @hr_manager_position_id, @volia_id, @jane_smith_id,
     '2021-07-15', 75000.00),
    
    (@hr_dept_id, @lead_generator_position_id, @volia_id, @emily_johnson_id,
     '2023-05-10', 70000.00);


INSERT INTO entity_address (entity_type, entity_id, address_id)
VALUES
    ('Company', @volia_id, @company_address_1_id),
    ('Company', @u_post_id, @company_address_2_id),
    ('Employee', @john_doe_id, @employee_address_1_id),
    ('Employee', @jane_smith_id, @employee_address_2_id);
