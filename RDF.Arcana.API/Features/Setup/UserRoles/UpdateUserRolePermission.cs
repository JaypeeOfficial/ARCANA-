using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;
using EntityFrameworkQueryableExtensions = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UpdateUserRolePermission
{
    public class UntagUserRoleCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public ICollection<string> Permissions { get; set; }
    }
    public class Handler : IRequestHandler<UntagUserRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UntagUserRoleCommand request, CancellationToken cancellationToken)
        {
            var existingUseRole = await _context.UserRoles
                .FirstOrDefaultAsync(x => x.Id == request.UserRoleId);

            if (existingUseRole is null)
            {
                throw new UserRoleNotFoundException();
            }

            existingUseRole.Permissions = request.Permissions;
            existingUseRole.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}