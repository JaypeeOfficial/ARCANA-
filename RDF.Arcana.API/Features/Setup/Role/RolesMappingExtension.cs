using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Role;

public static class RolesMappingExtension
{
    public static GetRolesAsync.GetRolesAsyncResult
        ToGetAllRolesAsyncResult(this Domain.Role role)
    {
        return new GetRolesAsync.GetRolesAsyncResult
        {
            RoleName = role.RoleName,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt,
            IsActive = role.IsActive,
            Users = role.Users.Select( x => new GetRolesAsync.Users
            {
                Fullname = x.Fullname,
            })
        };
    }
}