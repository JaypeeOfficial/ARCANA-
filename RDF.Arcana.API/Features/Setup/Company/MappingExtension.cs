namespace RDF.Arcana.API.Features.Setup.Company;

public static class MappingExtension
{
    public static GetAllCompaniesAsync
        .GetAllCompaniesResult ToGetAllCompaniesResult(this Domain.Company company)
    {
        return new GetAllCompaniesAsync.GetAllCompaniesResult
        {
            CompanyName = company.CompanyName,
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt,
            IsActive = company.IsActive
        };
    }
    
    public static GetCompaniesByStatusAsync.GetCompaniesByStatusResult
         ToGetAllCompaniesByStatusResult(this Domain.Company company)
    {
        return new GetCompaniesByStatusAsync.GetCompaniesByStatusResult
        {
            CompanyName = company.CompanyName,
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt,
            IsActive = company.IsActive
        };
    }
}