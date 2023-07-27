using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Company;

public class GetCompaniesAsync
{
    public class GetCompaniesQuery : UserParams, IRequest<PagedList<GetCompaniesResult>>
    {
        public bool? Status { get; set; }
        public string Search { get; set; }
    }

    public class GetCompaniesResult
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
     public class Handler : IRequestHandler<GetCompaniesQuery, PagedList<GetCompaniesResult>>
     {
         private readonly DataContext _context;
         
         public Handler(DataContext context)
         {
             _context = context;
         }
         
        public async Task<PagedList<GetCompaniesResult>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
           {
               var companies = _context.Companies.AsQueryable();
           
               
               if (!string.IsNullOrEmpty(request.Search))
               {
                   companies = companies.Where(x => x.CompanyName.Contains(request.Search));
               }
           
               if (request.Status != null)
               {
                   companies = companies.Where(x => x.IsActive == request.Status);
               }
           
               var result = companies.Select(x => x.ToGetAllCompaniesResult());
               
               return await PagedList<GetCompaniesResult>.CreateAsync(result, request.PageNumber, request.PageSize);
           }
     }
    
}