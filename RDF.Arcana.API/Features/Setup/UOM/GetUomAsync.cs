using MediatR;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.UOM;

public class GetUomAsync
{
    public class GetUomAsyncQuery : UserParams, IRequest<PagedList<GetUomQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetUomQueryResult
    {
        public int Id { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetUomAsyncQuery, PagedList<GetUomQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUomQueryResult>> Handle(GetUomAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Uom> uoms = _context.Uoms;

            if (!string.IsNullOrEmpty(request.Search))
            {
                uoms = uoms.Where(x => x.UomCode.Contains(request.Search));
            }

            if (request.Status != null)
            {
                uoms = uoms.Where(x => x.IsActive == request.Status);
            }

            var result = uoms.Select(x => x.ToGetUomQueryResult());

            return await PagedList<GetUomQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
}