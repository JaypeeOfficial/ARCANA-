﻿using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Users;

public static class MapperProfile
{
    public static GetUsersAsync.GetUserAsyncQueryResult 
        ToGetUserAsyncQueryResult (this User user)
    {
        return new GetUsersAsync.GetUserAsyncQueryResult
        {
            Fullname = user.Fullname,
            Username = user.Username,
            Password = user.Password,
            CreatedAt = user.CreatedAt,
            IsActive = user.IsActive,
            CompanyName = user.Company.CompanyName,
            DepartmentName = user.Department.DepartmentName,
            LocationName = user.Location.LocationName,
            RoleName = user.UserRoles.RoleName,
            Permission = user.UserRoles.Permissions
        };
    }
}