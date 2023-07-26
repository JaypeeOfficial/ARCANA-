using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public class GetProductSubCategories
{
    public class GetProductSubCategoriesQuery : UserParams, IRequest<PagedList<GetProductSubCategoriesResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetProductSubCategoriesResult
    {
        public string ProductSubCategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetProductSubCategoriesQuery, PagedList<GetProductSubCategoriesResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetProductSubCategoriesResult>> Handle(GetProductSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductSubCategory> productSubCategories =
                _context.ProductSubCategories.Include(x => x.ProductCategory);

            if (!string.IsNullOrEmpty(request.Search))
            {
                productSubCategories =
                    productSubCategories.Where(p => p.ProductSubCategoryName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                productSubCategories = productSubCategories.Where(x => x.IsActive == request.Status);
            }

            var result = productSubCategories.Select(x => x.GetProductSubCategoriesResult());

            return await PagedList<GetProductSubCategoriesResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}