using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

public class UpdateItemStatus
{
    public class UpdateItemStatusCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public bool Status { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateItemStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateItemStatusCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
        
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
        
            item.IsActive = !item.IsActive;
            item.ModifiedBy = request.ModifiedBy;
        
            await _context.SaveChangesAsync(cancellationToken);
                
            return Unit.Value;
        }
    }
}