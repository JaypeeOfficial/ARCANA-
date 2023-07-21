namespace RDF.Arcana.API.Features.Setup.Company;

public static class MappingExtension
{
    
    public static GetCompaniesAsync.GetCompaniesResult
         ToGetAllCompaniesResult(this Domain.Company company)
    {
        return new GetCompaniesAsync.GetCompaniesResult
        {
            CompanyName = company.CompanyName,
            CreatedAt = company.CreatedAt,
            UpdatedAt = company.UpdatedAt,
            IsActive = company.IsActive
        };
    }
}