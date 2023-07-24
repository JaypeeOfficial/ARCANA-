using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Role.Exception;

namespace RDF.Arcana.API.Features.Setup.Role;

public class UpdateRoleStatus
{
    public class UpdateRoleStatusCommand : IRequest<Unit>
    {
        public int RoleId { get; set; }
        public bool Status { get; set; }
    }
    public class Handler : IRequestHandler<UpdateRoleStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRoleStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);

            if (existingRole is null)
            {
                throw new NoRoleFoundException();
            }

            existingRole.IsActive = request.Status;
            existingRole.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}