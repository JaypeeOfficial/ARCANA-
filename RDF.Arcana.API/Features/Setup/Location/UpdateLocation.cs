using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

public abstract class UpdateLocation
{
    public class UpdateLocationCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);

            if (location == null)
            {
                throw new NoLocationFoundException();
            }

            location.LocationName = request.LocationName;
            location.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}