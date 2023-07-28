using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public class AddNewProductSubCategory
{
    public class AddNewProductSubCategoryCommand : IRequest<Unit>
    {
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
    }
    public class Handler : IRequestHandler<AddNewProductSubCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProductSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductSubCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(
                    x => x.ProductSubCategoryName == request.ProductSubCategoryName, cancellationToken);

            if (existingProductSubCategory != null)
            {
                throw new ProductSubCategoryAlreadyExistException(request.ProductSubCategoryName);
            }

            var productSubCategory = new ProductSubCategory
            {
                ProductSubCategoryName = request.ProductSubCategoryName,
                ProductCategoryId = request.ProductCategoryId,
                IsActive = true
            };

            await _context.ProductSubCategories.AddAsync(productSubCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}