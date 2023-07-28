using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

public class UpdateItem
{
    public class UpdateItemCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int UomId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int MeatTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateItemCommand, Unit>
    {
        private readonly DataContext _context;
    
        public Handler(DataContext context)
        {
            _context = context;
        }
    
        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
    
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
    
            item.ItemDescription = request.ItemDescription;
            item.UomId = request.UomId;
            item.ProductSubCategoryId = request.ProductSubCategoryId;
            item.MeatTypeId = request.MeatTypeId;
    
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}