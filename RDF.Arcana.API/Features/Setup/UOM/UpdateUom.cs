using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

public class UpdateUom
{
    public class UpdateUomCommand : IRequest<Unit>
    {
        public int UomId { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateUomCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                throw new UomNotFoundException();
            }

            existingUom.UomCode = request.UomCode;
            existingUom.UomDescription = request.UomDescription;
            existingUom.UpdatedAt = DateTime.Now;
            existingUom.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}