
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class AddNewUserRoles
{
    public class AddNewUserRolesCommand : IRequest<Unit>
    {
        public string RoleName { get; set; }
        public List<string> Permission { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewUserRolesCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewUserRolesCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleName == request.RoleName, cancellationToken);
            if (existingUserRole is not null)
            {
                throw new UserRoleAlreadyExistException();
            }

            var userRole = new Domain.UserRoles
            {
                RoleName = request.RoleName,
                Permissions = request.Permission,
                IsActive = request.IsActive
            };

            await _context.UserRoles.AddAsync(userRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}