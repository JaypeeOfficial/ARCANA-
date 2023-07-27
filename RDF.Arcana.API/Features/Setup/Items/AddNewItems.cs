using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

public class AddNewItems
{
    public class AddNewItemsCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int UomId { get; set; }
        public int ProductCategoryId { get; set; }
        public int MeatTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewItemsCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewItemsCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);

            if (existingItem is not null)
            {
                throw new ItemAlreadyExistException();
            }

            var items = new Domain.Items
            {
                ItemCode = request.ItemCode,
                ItemDescription = request.ItemDescription,
                UomId = request.UomId,
                ProductCategoryId = request.ProductCategoryId,
                MeatTypeId = request.MeatTypeId,
                IsActive = true
            };

            await _context.Items.AddAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
}