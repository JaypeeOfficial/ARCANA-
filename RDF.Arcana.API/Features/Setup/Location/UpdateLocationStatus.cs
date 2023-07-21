using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Location;

public class UpdateLocationStatus
{
    public class UpdateLocationStatusCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
        public bool Status { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationStatusCommand request, CancellationToken cancellationToken)
        {
            var validateLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId,
                    cancellationToken);

            if (validateLocation is not null)
            {
                validateLocation.IsActive = request.Status;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}