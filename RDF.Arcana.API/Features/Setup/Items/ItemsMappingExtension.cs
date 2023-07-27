namespace RDF.Arcana.API.Features.Setup.Items;

public static class ItemsMappingExtension
{
    public static GetItemsAsync.GetItemsAsyncResult
        ToGetItemsAsyncResult(this Domain.Items items)
    {
        return new GetItemsAsync.GetItemsAsyncResult
        {
            Id = items.Id,
            ItemCode = items.ItemCode,
            ItemDescription = items.ItemDescription,
            UomId = items.UomId,
            ProductCategoryId = items.ProductCategoryId,
            MeatTypeId = items.MeatTypeId,
            IsActive = items.IsActive,
            AddedBy = items.AddedBy,
            ModifiedBy = items.ModifiedBy
        };
    }
}