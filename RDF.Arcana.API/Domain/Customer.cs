using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Customer : BaseEntity
{
    public string Fullname { get; set; }
    public bool IsActive { get; set; }
}