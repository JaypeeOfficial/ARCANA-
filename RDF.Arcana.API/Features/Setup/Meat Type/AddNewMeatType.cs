using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

public class AddNewMeatType
{
    public class AddNewMeatTypeCommand : IRequest<Unit>
    {
        public string MeatTypeName { get; set; }
        public string AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewMeatTypeCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewMeatTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.MeatTypeName == request.MeatTypeName,cancellationToken);

            if (existingMeatType is not null)
            {
                throw new MeatTypeIsAlreadyExistException(request.MeatTypeName);
            }

            var meatType = new MeatType
            {
                MeatTypeName = request.MeatTypeName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.MeatTypes.AddAsync(meatType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}