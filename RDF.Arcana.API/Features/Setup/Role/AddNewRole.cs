using Microsoft.EntityFrameworkCore;
using MediatR;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Role.Exception;

namespace RDF.Arcana.API.Features.Setup.Role;

public class AddNewRole
{
    public class AddNewRoleCommand : IRequest<Unit>
    {
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
    public class Handler : IRequestHandler<AddNewRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExist = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == request.RoleName, cancellationToken);

            if (roleExist != null)
            {
                throw new RoleAlreadyExist(request.RoleName);
            }

            var roles = new Domain.Role
            {
                RoleName = request.RoleName,
                IsActive = request.IsActive
            };
            await _context.Roles.AddAsync(roles, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}