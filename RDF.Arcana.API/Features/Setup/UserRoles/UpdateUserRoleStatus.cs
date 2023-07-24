using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UpdateUserRoleStatus
{
    public class UpdateUserRoleStatusCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public bool Status { get; set; }
    }
    public class Handler : IRequestHandler<UpdateUserRoleStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserRoleStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (existingUserRole is null)
            {
                throw new UserRoleNotFoundException();
            }

            if (request.Status == false && existingUserRole.Permissions.Count > 0)
            {
                throw new UserRoleDeactivationException();
            }

            existingUserRole.IsActive = request.Status;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}