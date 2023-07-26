using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

public class AddNewProductCategory
{
    public class AddNewProductCategoryCommand : IRequest<Unit>
    {
        public string ProductCategoryName { get; set; }
        public string AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewProductCategoryCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(
                    x => x.ProductCategoryName == request.ProductCategoryName, cancellationToken);

            if (existingProductCategory is not null)
            {
                throw new ProductCategoryAlreadyExistException();
            }

            var productCategory = new ProductCategory
            {
                ProductCategoryName = request.ProductCategoryName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.ProductCategories.AddAsync(productCategory, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}