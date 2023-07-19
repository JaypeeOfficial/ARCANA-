using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Items : BaseEntity
{
    public string ItemCode { get; set; }
    public string ItemDescription { get; set; }
    public int UomId { get; set; }
    public int ProductCategoryId { get; set; }
    public int MeatTypeId { get; set; }
    public bool IsActive { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public Uom Uom { get; set; }
    public MeatType MeatType { get; set; }
}