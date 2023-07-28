using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

public class AddNewUom
{
    public class AddNewUomCommand : IRequest<Unit>
    {
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public string AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewUomCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom =
                await _context.Uoms.FirstOrDefaultAsync(x => x.UomCode == request.UomCode, cancellationToken);
            if (existingUom is not null)
            {
                throw new UomAlreadyExistException();
            }

            var uom = new Uom
            {
                UomCode = request.UomCode,
                UomDescription = request.UomDescription,
                IsActive = true,
                AdddedBy = request.AddedBy
            };
            await _context.Uoms.AddAsync(uom, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}