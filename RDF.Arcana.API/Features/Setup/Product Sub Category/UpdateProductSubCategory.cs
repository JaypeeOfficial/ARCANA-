using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public class UpdateProductSubCategory
{
    public class UpdateProductSubCategoryCommand : IRequest<Unit>
    {
        public int ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductSubCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductSubCategory =
                await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.Id == request.ProductSubCategoryId,
                    cancellationToken);
            var validateProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            if (existingProductSubCategory == null)
            {
                throw new NoProductSubCategoryFoundException();
            }

            if (validateProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            existingProductSubCategory.ProductSubCategoryName = request.ProductSubCategoryName;
            existingProductSubCategory.ProductCategoryId = request.ProductCategoryId;
            existingProductSubCategory.ModifiedBy = request.ModifiedBy;
            existingProductSubCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}