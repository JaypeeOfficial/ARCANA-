using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Role : BaseEntity
{
    public string RoleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual User User { get; set; }
}