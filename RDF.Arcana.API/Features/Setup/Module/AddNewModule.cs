using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Module.Exception;

namespace RDF.Arcana.API.Features.Setup.Module;

public class AddNewModule
{
    public class AddNewModuleAsync : IRequest<Unit>
    {
        public int MainMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string ModulePath { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class Handler : IRequestHandler<AddNewModuleAsync, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewModuleAsync request, CancellationToken cancellationToken)
        {
            var existingModule =
                await _context.Modules.FirstOrDefaultAsync(x => x.ModulePath == request.ModulePath, cancellationToken);
            if (existingModule is not null)
            {
                throw new ModulePathAlreadyExistException(request.ModulePath);
            }

            var module = new Domain.Module
            {
                MainMenuId = request.MainMenuId,
                SubMenuName = request.SubMenuName,
                ModulePath = request.ModulePath,
                AddedBy = request.AddedBy,
                IsActive = request.IsActive
            };

            await _context.Modules.AddAsync(module, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}