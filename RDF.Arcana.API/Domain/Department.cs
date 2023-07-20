using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual User User { get; set; }
}