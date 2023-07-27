using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

public class GetDiscountsAsync
{
    public class GetDiscountAsyncQuery : UserParams, IRequest<PagedList<GetDiscountAsyncQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetDiscountAsyncQueryResult
    {
        public int Id { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetDiscountAsyncQuery, PagedList<GetDiscountAsyncQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDiscountAsyncQueryResult>> Handle(GetDiscountAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Discount> discounts = _context.Discounts;

            if (!string.IsNullOrEmpty(request.Search))
            {
                discounts = discounts.Where(x => x.AddedBy.Contains(request.Search));
            }

            if (request.Status != null )
            {
                discounts = discounts.Where(x => x.IsActive == request.Status);
            }

            var result = discounts.Select(x => x.ToGetDiscountAsyncQueryResult());
            return await PagedList<GetDiscountAsyncQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}