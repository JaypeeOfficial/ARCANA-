using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Customer : BaseEntity
{
    public string Fullname { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public int LocationId { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; }
    public Company Company { get; set; }
    public Department Department { get; set; }
    public Location Location { get; set; }
    public Role Role { get; set; }
}