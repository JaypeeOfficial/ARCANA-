using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ProductCategory : BaseEntity
{
    public string ProductCategoryName { get; set; }
    public int ProductSubCategoryId { get; set; }
    public string AddedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public ProductSubCategory ProductSubCategory { get; set; }
}