using System.Data.Entity;
using MediatR;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Module.Exception;
using RDF.Arcana.API.Features.Setup.Role;

namespace RDF.Arcana.API.Features.Setup.Module;

public class UpdateModuleStatus
{
    public class UpdateModuleStatusCommand : IRequest<Unit>
    {
        public int ModuleId { get; set; }
        public bool Status { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateModuleStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateModuleStatusCommand request, CancellationToken cancellationToken)
        {
            var existingModule =
                await _context.Modules.FirstOrDefaultAsync(x => x.Id == request.ModuleId, cancellationToken);

            if (existingModule is null)
            {
                throw new ModuleNotFoundException();
            }

            existingModule.IsActive = request.Status;
            existingModule.UpdatedAt = DateTime.Now;
            existingModule.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}