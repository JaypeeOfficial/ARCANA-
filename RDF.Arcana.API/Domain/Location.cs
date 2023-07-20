using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Location : BaseEntity
{
    public string LocationName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual User User { get; set; }
}