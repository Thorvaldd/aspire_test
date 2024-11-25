
CREATE PROCEDURE add_person(
    IN firstName VARCHAR(100),      
    IN lastName VARCHAR(100),       
    IN dob DATE,                    
    IN addressLine1 VARCHAR(255),   
    IN addressLine2 VARCHAR(255),   
    IN city VARCHAR(100),           
    IN state VARCHAR(100),          
    IN postalCode VARCHAR(20),      
    IN country VARCHAR(100)         
)
BEGIN
    DECLARE newPersonId INT;        
    DECLARE newAddressId INT;       
    DECLARE employeeAddressTypeId INT;

    SET employeeAddressTypeId = (SELECT address_type_id FROM dict_address_type WHERE name = 'Employee');

    INSERT INTO person (first_name, last_name, dob)
    VALUES (firstName, lastName, dob);
    SET newPersonId = LAST_INSERT_ID();

    INSERT INTO address (line_1, line_2, city, state, postal_code, country, address_type_id)
    VALUES (addressLine1, addressLine2, city, state, postalCode, country, employeeAddressTypeId);
    SET newAddressId = LAST_INSERT_ID();

    INSERT INTO entity_address (entity_type, entity_id, address_id)
    VALUES ('Employee', newPersonId, newAddressId);
END
