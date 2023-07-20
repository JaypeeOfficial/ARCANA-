namespace RDF.Arcana.API.Features.Users;

public static class UserMappingExtension
{
    public static GetAllUsersAsync.GetAllUserAsyncResult
 ToGetAllUserAsyncResult(this Domain.User user)
    {
        return new GetAllUsersAsync.GetAllUserAsyncResult
        {
            Fullname = user.Fullname,
            Username = user.Username,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            CompanyName = user.Company.CompanyName,
            DepartmentName = user.Department.DepartmentName,
            LocationName = user.Location.LocationName,
            RoleName = user.Role.RoleName,
            IsActive = user.IsActive
            
        };
    }
}