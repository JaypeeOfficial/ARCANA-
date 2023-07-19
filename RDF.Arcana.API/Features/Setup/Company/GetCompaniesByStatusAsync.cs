using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Company;

public class GetCompaniesByStatusAsync
{
    public class GetCompaniesByStatusQuery : UserParams, IRequest<PagedList<GetCompaniesByStatusResult>>
    {
        public bool Status { get; set; }
    }

    public class GetCompaniesByStatusResult
    {
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
     public class Handler : IRequestHandler<GetCompaniesByStatusQuery, PagedList<GetCompaniesByStatusResult>>
     {
         private readonly DataContext _context;
         
         public Handler(DataContext context)
         {
             _context = context;
         }
         
         public async Task<PagedList<GetCompaniesByStatusResult>> Handle(GetCompaniesByStatusQuery request, CancellationToken cancellationToken)
         {
             var companies = _context.Companies.Where(x => x.IsActive == request.Status);

             var result = companies.Select(x => x.ToGetAllCompaniesByStatusResult());

             return await PagedList<GetCompaniesByStatusResult>.CreateAsync(result, request.PageNumber,
                 request.PageNumber);

         }
     }
    
}