using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

public class UpdateMeatTypeStatus
{
    public class UpdateMeatTypeStatusCommand : IRequest<Unit>
    {
        public int MeatTypeId { get; set; }
        public bool Status { get; set; }
    }

    public class Handler : IRequestHandler<UpdateMeatTypeStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMeatTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (existingMeatType is null)
            {
                throw new MeatTypeNotFoundException();
            }

            existingMeatType.IsActive = !existingMeatType.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}