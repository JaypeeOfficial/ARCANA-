using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public static class ProductSubCategoryMappingExtension
{
    public static GetProductSubCategories.GetProductSubCategoriesResult
        GetProductSubCategoriesResult(this ProductSubCategory productSubCategory)
    {
        return new GetProductSubCategories.GetProductSubCategoriesResult
        {
            ProductSubCategoryName = productSubCategory.ProductSubCategoryName,
            ProductCategoryName = productSubCategory.ProductCategory.ProductCategoryName,
            CreatedAt = productSubCategory.CreatedAt,
            UpdatedAt = productSubCategory.UpdatedAt,
            ModifiedBy = productSubCategory.ModifiedBy,
            IsActive = productSubCategory.IsActive
        };
    }
}