using MediatR;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Items;

public class GetItemsAsync
{
    public class GetItemsAsyncQuery : UserParams, IRequest<PagedList<GetItemsAsyncResult>>
        {
            public string Search { get; set; }
            public bool? IsActive { get; set; }
        }
    
        public class GetItemsAsyncResult
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public int UomId { get; set; }
            public int ProductCategoryId { get; set; }
            public int MeatTypeId { get; set; }
            public bool IsActive { get; set; }
            public string AddedBy { get; set; }
            public string ModifiedBy { get; set; }
        }
    
        public class Handler : IRequestHandler<GetItemsAsyncQuery, PagedList<GetItemsAsyncResult>>
        {
            private readonly DataContext _context;
    
            public Handler(DataContext context)
            {
                _context = context;
            }
    
            public async Task<PagedList<GetItemsAsyncResult>> Handle(GetItemsAsyncQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Domain.Items> items = _context.Items;
    
                if (!string.IsNullOrEmpty(request.Search))
                {
                    items = items
                        .Where(i => i.ItemCode.Contains(request.Search) || i.ItemDescription.Contains(request.Search));
                }
    
                if (request.IsActive != null)
                {
                    items = items
                        .Where(i => i.IsActive == request.IsActive);
                }

                var result = items.Select(i => i.ToGetItemsAsyncResult());
    
                return await PagedList<GetItemsAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
}