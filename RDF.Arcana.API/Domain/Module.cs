using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Module : BaseEntity
{
    public int MainMenuId { get; set; }
    public string SubMenuName { get; set; }
    public string ModulePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public string AddedBy { get; set; }
    public bool IsActive { get; set; }
    public string ModifiedBy { get; set; }
    public MainMenu MainMenu { get; set; }
}