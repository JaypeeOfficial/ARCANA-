using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

public class UpdateMeatType
{
    public class UpdateMeatTypeCommand : IRequest<Unit>
    {
        public int MeatTypeId { get; set; }
        public string MeatTypeName { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateMeatTypeCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMeatTypeCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType = await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            var validateMeatTypeName =
                await _context.MeatTypes.Where(x => x.MeatTypeName == request.MeatTypeName).FirstOrDefaultAsync(cancellationToken);

            if (validateMeatTypeName is null)
            {
                existingMeatType.MeatTypeName = request.MeatTypeName;
                existingMeatType.ModifiedBy = request.ModifiedBy;
                existingMeatType.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;

            }
            
            if (validateMeatTypeName.MeatTypeName == request.MeatTypeName && validateMeatTypeName.Id == request.MeatTypeId)
            {
                throw new Exception("No changes");
            }
            
            if (validateMeatTypeName.MeatTypeName == request.MeatTypeName && validateMeatTypeName.Id != request.MeatTypeId)
            {
                throw new MeatTypeIsAlreadyExistException(request.MeatTypeName);
            }
           
            
            return Unit.Value;
        }
    }
}