using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

public class UpdateProductCategoryStatus
{
    public class UpdateProductCategoryStatusCommand : IRequest<Unit>
    {
        public int ProductCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProductCategoryStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCategoryStatusCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            if (existingProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            existingProductCategory.IsActive = request.IsActive;
            existingProductCategory.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}