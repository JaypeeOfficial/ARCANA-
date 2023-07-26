using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

public class UpdateProductCategory
{
    public class UpdateProductCategoryCommand : IRequest<Unit>
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
    public class Handler : IRequestHandler<UpdateProductCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);

            if (existingProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            existingProductCategory.ProductCategoryName = request.ProductCategoryName;
            existingProductCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}