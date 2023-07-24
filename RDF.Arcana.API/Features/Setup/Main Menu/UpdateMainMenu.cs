using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Main_Menu.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

public class UpdateMainMenu
{
    public class UpdateMainMenuCommand : IRequest<Unit>
    {
        public int MainMenuId { get; set; }
        public string ModuleName { get; set; }
        public string ModifiedBy { get; set; }
        public string MenuPath { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateMainMenuCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMainMenuCommand request, CancellationToken cancellationToken)
        {
            var existingMainMenu = await _context.MainMenus.FirstOrDefaultAsync(x => x.Id == request.MainMenuId, cancellationToken);

            if (existingMainMenu is null)
            {
                throw new NoMainMenuFound();
            }

            existingMainMenu.ModuleName = request.ModuleName;
            existingMainMenu.MenuPath = request.MenuPath;
            existingMainMenu.ModifiedBy = request.ModifiedBy;
            existingMainMenu.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}