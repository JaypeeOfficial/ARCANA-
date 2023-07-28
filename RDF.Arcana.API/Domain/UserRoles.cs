using Microsoft.AspNetCore.Http.HttpResults;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Domain;

public class UserRoles : BaseEntity
{
    public string UserRoleName { get; set; }
    public ICollection<string> Permissions { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public string AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public virtual User User { get; set; }
}