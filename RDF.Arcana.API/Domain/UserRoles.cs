using Microsoft.AspNetCore.Http.HttpResults;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Domain;

public class UserRoles : BaseEntity
{
    public string RoleName { get; set; }
    public ICollection<string> Permissions { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual User User { get; set; }
}