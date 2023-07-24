using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public static class UserRoleMappingExtension
{
    public static GetUserRolesAsync.GetUserRoleAsyncResult
        ToGetUserRoleAsyncQueryResult(this Domain.UserRoles userRoles)
    {
        return new GetUserRolesAsync.GetUserRoleAsyncResult()
        {
            RoleName = userRoles.RoleName,
            CreatedAt = userRoles.CreatedAt,
            IsActive = userRoles.IsActive,
            UpdatedAt = userRoles.UpdatedAt,
            Permissions = userRoles.Permissions,
            User = userRoles.User.Fullname
        };
    }
}