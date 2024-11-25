namespace Emp.EmployeesDb.Entities;

public class Address
{
    public int AddressId { get; set; }
    public string Line1 { get; set; } = null!;
    public string? Line2 { get; set; }
    public string City { get; set; } = null!;
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    #region Fk to AddressType
    public int AddressTypeId { get; set; }
    public AddressType AddressType { get; set; } = null!;
    #endregion
}