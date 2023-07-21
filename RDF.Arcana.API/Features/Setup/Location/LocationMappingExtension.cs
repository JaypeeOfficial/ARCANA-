namespace RDF.Arcana.API.Features.Setup.Location;

public static class LocationMappingExtension
{
    public static GetAllLocationAsync.GetAllLocationAsyncResult
        ToGetAllLocationResult(this Domain.Location location)
    {
        return new GetAllLocationAsync.GetAllLocationAsyncResult
        {
            LocationName = location.LocationName,
            CreatedAt = location.CreatedAt.ToString("MM/dd/yyyy"),
            UpdatedAt = location.UpdatedAt.ToString(),
            IsActive = location.IsActive,
            Users = location.Users.Select(x => x.Fullname).ToList()
        };
    }
}