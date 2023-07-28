using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

public class UpdateLocationStatus
{
    public class UpdateLocationStatusCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
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

            if (validateLocation is null)
            {
                throw new NoLocationFoundException();
            }

            validateLocation.IsActive = !validateLocation.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}