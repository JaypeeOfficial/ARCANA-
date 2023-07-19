using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ProductSubCategory : BaseEntity
{
    public string ProductSubCategoryName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }
}