USE employees;

CREATE TABLE IF NOT EXISTS dict_address_type (
    address_type_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL 
);

CREATE TABLE IF NOT EXISTS address(
    address_id INT PRIMARY KEY AUTO_INCREMENT,
    line_1 VARCHAR(255) NOT NULL ,
    line_2 VARCHAR(255),
    city VARCHAR(100) NOT NULL ,
    state VARCHAR(100) ,
    postal_code VARCHAR(100) NULL ,
    country VARCHAR(100) ,
    address_type_id INT NOT NULL ,
    FOREIGN KEY (address_type_id) REFERENCES dict_address_type(address_type_id)
);

CREATE TABLE IF NOT EXISTS entity_address(
    entity_address_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    entity_type VARCHAR(100) NOT NULL , -- Determine to what entity 'Company', 'Employee' address belongs
    entity_id INT NOT NULL , -- 'Entity relation
    address_id INT NOT NULL ,
    FOREIGN KEY (address_id) REFERENCES address(address_id)
);

CREATE TABLE IF NOT EXISTS company(
    company_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL ,
    company_logo TEXT
);
CREATE TABLE IF NOT EXISTS department(
    department_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL ,
    company_id INT NOT NULL ,
    FOREIGN KEY (company_id) REFERENCES company(company_id)
);

CREATE TABLE IF NOT EXISTS dict_position(
    position_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL 
);


CREATE TABLE IF NOT EXISTS person(
                                     person_id INT PRIMARY KEY AUTO_INCREMENT,
                                     first_name VARCHAR(100) NOT NULL ,
                                     last_name VARCHAR(100) NOT NULL,
                                     full_name VARCHAR(255) AS(CONCAT(first_name, ' ', last_name)),
                                     dob DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS employee(
    employee_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    department_id INT NOT NULL,
    position_id INT NOT NUll,
    company_id INT NOT NULL,
    person_id INT NOT NULL ,
    start_job_date DATETIME NOT NULL ,
    salary DECIMAL(10,2) NOT NULL ,
    
    FOREIGN KEY (department_id) REFERENCES department(department_id),
    FOREIGN KEY (position_id) REFERENCES dict_position(position_id),
    FOREIGN KEY (company_id) REFERENCES company(company_id),
    FOREIGN KEY (person_id) REFERENCES person(person_id)
);

