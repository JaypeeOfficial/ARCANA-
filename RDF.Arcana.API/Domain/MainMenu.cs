using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class MainMenu : BaseEntity
{
    public string ModuleName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public string AddedBy { get; set; }
    public bool IsActive { get; set; }
    public string ModifiedBy { get; set; }
    public string MenuPath { get; set; }
}