using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category
{
    public class UpdateProductSubCategoryStatus
    {
        public class UpdateProductSubCategoryStatusCommand : IRequest<Unit>
        {
            public int ProductSubCategoryId { get; set; }
        }

        public class Handler : IRequestHandler<UpdateProductSubCategoryStatusCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductSubCategoryStatusCommand request,
                CancellationToken cancellationToken)
            {
                var existingProductSubCategory =
                    await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId,
                        cancellationToken);
                if (existingProductSubCategory == null)
                {
                    throw new NoProductSubCategoryFoundException();
                }
                
                existingProductSubCategory.IsActive = !existingProductSubCategory.IsActive;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}