using Emp.EmployeesDb.Entities.Enums;

namespace Emp.EmployeesDb.Entities;

public class EntityAddress
{
    public long EntityAddressId { get; set; }
    public EntityType EntityType { get; set; }
    
    /// <summary>
    /// Fk will be defined in application code
    /// </summary>
    public int EntityTypeId { get; set; }

    #region FK to address
    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;
    #endregion
}