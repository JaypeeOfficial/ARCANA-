using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class MeatType : BaseEntity
{
    public string MeatTypeName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Type { get; set; }
    public bool IsActive { get; set; }
}