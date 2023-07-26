using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

public class UpdateUomStatus
{
    public class UpdateUomStatusCommand : IRequest<bool>
    {
        public int UomId { get; set; }
        public bool Status { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUomStatusCommand, bool>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUomStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                throw new UomNotFoundException();
            }

            existingUom.IsActive = request.Status;
            existingUom.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}