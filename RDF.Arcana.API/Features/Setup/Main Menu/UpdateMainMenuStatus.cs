using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Main_Menu.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

public class UpdateMainMenuStatus
{
    public class UpdateMainMenuStatusCommand : IRequest<Unit>
    {
        public int MainMenuId { get; set; }
        public bool Status { get; set; }
    }

    public class Handler : IRequestHandler<UpdateMainMenuStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMainMenuStatusCommand request, CancellationToken cancellationToken)
        {
            var existingMainMenu = await _context.MainMenus.FirstOrDefaultAsync(x => x.Id == request.MainMenuId, cancellationToken);

            if (existingMainMenu is null)
            {
                throw new NoMainMenuFound();
            }

            existingMainMenu.IsActive = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}