using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Location;

public class AddNewLocation
{
    public class AddNewLocationCommand : IRequest<Unit>
    {
        public string LocationName { get; set; }
        public bool Status { get; set; }
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
                await _context.Locations.SingleOrDefaultAsync(x => x.LocationName == request.LocationName,
                    cancellationToken);
            if (existingLocation is not null)
            {
                existingLocation.IsActive = request.Status;
                existingLocation.LocationName = existingLocation.LocationName;
                _context.Locations.Attach(existingLocation).State = EntityState.Modified;
            }
            else
            {
                var location = new Domain.Location
                {
                    LocationName = request.LocationName,
                    IsActive = request.Status,
                };

                await _context.Locations.AddAsync(location, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}