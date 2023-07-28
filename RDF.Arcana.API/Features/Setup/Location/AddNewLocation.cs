using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

public class AddNewLocation
{
    public class AddNewLocationCommand : IRequest<Unit>
    {
        public string LocationName { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewLocationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewLocationCommand request, CancellationToken cancellationToken)
        {
            var existingLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.LocationName == request.LocationName,
                    cancellationToken);
            if (existingLocation is not null)
            {
                throw new LocationAlreadyExist();
            }
           
            var location = new Domain.Location
                {
                    LocationName = request.LocationName,
                    IsActive = true
                };

            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}