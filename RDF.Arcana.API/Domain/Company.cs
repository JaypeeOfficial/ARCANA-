using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Company : BaseEntity
{
    public string CompanyName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual Customer Customer { get; set; }
}