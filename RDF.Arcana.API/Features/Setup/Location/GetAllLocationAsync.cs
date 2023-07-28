using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Location;

public class GetAllLocationAsync
{
    public class GetAllLocationAsyncQuery : UserParams, IRequest<PagedList<GetAllLocationAsyncResult>>
    {
        public bool Status { get; set; }
        public string Search { get; set; }
    }

    public class GetAllLocationAsyncResult
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public List<string> Users { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllLocationAsyncQuery, PagedList<GetAllLocationAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllLocationAsyncResult>> Handle(GetAllLocationAsyncQuery request, CancellationToken cancellationToken)
        {
           var locations = _context.Locations.Where(x => x.IsActive == request.Status);
            
            var location = !string.IsNullOrEmpty(request.Search) ? 
                locations.Where(x => x.LocationName.Contains(request.Search)) : 
                locations.Where(x => x.IsActive == request.Status);

            var result = location.Select(l => l.ToGetAllLocationResult());

            return await PagedList<GetAllLocationAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            
        }
    }
}