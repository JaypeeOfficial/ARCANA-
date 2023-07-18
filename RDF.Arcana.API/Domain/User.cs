using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class User : BaseEntity
{
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}