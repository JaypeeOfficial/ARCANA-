
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Module.Exception;

namespace RDF.Arcana.API.Features.Setup.Module;

public class UpdateModule
{
    public class UpdateModuleCommand : IRequest<Unit>
    {
        public int ModuleId { get; set; }
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string ModulePath { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
    public class Handler : IRequestHandler<UpdateModuleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var existingModule =
                await _context.Modules.FirstOrDefaultAsync(x => x.Id == request.ModuleId, cancellationToken);

            if (existingModule is null)
            {
                throw new ModuleNotFoundException();
            }

            existingModule.MainMenuId = request.MainMenuId;
            existingModule.SubMenuName = request.SubMenuName;
            existingModule.ModulePath = request.ModulePath;
            existingModule.ModifiedBy = request.ModifiedBy;
            existingModule.IsActive = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
            
        }
    }
}