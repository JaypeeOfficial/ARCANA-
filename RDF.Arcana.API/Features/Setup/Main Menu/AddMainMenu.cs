
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Main_Menu.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

public class AddMainMenu
{
    public class AddMainMenuCommand : IRequest<Unit>
    {
        public string ModuleName { get; set; }

        public string AddedBy { get; set; }

        public string MenuPath { get; set; }
    }

    public class Handler : IRequestHandler<AddMainMenuCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddMainMenuCommand request, CancellationToken cancellationToken)
        {
            var existingMenuPath = await _context.MainMenus.FirstOrDefaultAsync(x => x.MenuPath == request.MenuPath, cancellationToken);
            var existingModuleName =
                await _context.MainMenus.FirstOrDefaultAsync(x => x.ModuleName == request.ModuleName, cancellationToken);

            if (existingMenuPath is not null)
                throw new MenuPathAlreadyExist(request.MenuPath);
            if (existingModuleName is not null)
                throw new MenuNameAlreadyExist(request.ModuleName);

            var menu = new MainMenu
            {
                ModuleName = request.ModuleName,
                AddedBy = request.AddedBy,
                IsActive = true,
                MenuPath = request.MenuPath
            };

            await _context.MainMenus.AddAsync(menu, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}