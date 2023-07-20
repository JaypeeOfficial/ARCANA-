using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

public class UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public int LocationId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validateUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (validateUser is null)
            {
                throw new UserNotExist();
            }

            validateUser.Fullname = request.Fullname;
            validateUser.Username = request.Username;
            validateUser.DepartmentId = request.DepartmentId;
            validateUser.CompanyId = request.CompanyId;
            validateUser.RoleId = request.RoleId;
            validateUser.LocationId = request.LocationId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}