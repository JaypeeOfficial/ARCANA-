using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Uom : BaseEntity
{
    public string UomCode { get; set; }
    public string UomDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}