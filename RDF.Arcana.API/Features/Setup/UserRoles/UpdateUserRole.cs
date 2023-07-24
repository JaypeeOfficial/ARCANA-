using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (existingUserRole is null)
            {
                throw new UserRoleNotFoundException();
            }
            //Add validation mus have 1 permission if it tagged to a user

            existingUserRole.RoleName = request.RoleName;
            existingUserRole.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}