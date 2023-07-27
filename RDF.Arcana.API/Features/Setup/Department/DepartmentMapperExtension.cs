namespace RDF.Arcana.API.Features.Setup.Department;

public static class DepartmentMapperExtension
{
    public static GetDepartmentAsync.GetDepartmentAsyncResult
         ToGetAllDepartmentAsyncResult(this Domain.Department department)
    {
        return new GetDepartmentAsync.GetDepartmentAsyncResult
        {
            Id = department.Id,
            DepartmentName = department.DepartmentName,
            CreatedAt = department.CreatedAt,
            UpdatedAt = department.UpdatedAt,
            IsActive = department.IsActive,
            Users = department.Users.Select(user => user.Fullname).ToList()
        };
    }
}