using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Role.Exception;

namespace RDF.Arcana.API.Features.Setup.Role;

public class UpdateRole
{
    public class UpdateRoleCommand : IRequest<Unit>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

            if (existingRole is null)
            {
                throw new NoRoleFoundException();
            }

            existingRole.RoleName = request.RoleName;
            existingRole.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}