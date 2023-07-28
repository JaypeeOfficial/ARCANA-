using Org.BouncyCastle.Tsp;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Setup.Department.Exception;
using RDF.Arcana.API.Features.Setup.Location.Exception;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

public class UpdateUser
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int? UserRoleId { get; set; }
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            
            var validateUsername =
                await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
            var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            var validateLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);
            var validateUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);
            
            if (validateUsername is not null)
                throw new UsernameAlreadyExistException();
            if (validateCompany is null)
                throw new NoCompanyFoundException();
            if (validateDepartment is null)
                throw new NoDepartmentFoundException();
            if (validateLocation is null)
                throw new NoLocationFoundException();
            if (validateUserRole is null)
                throw new UserRoleNotFoundException();

            user.Fullname = request.Fullname;
            user.Username = request.Username;
            user.CompanyId = request.CompanyId;
            user.LocationId = request.LocationId;
            user.DepartmentId = request.DepartmentId;
            user.UserRoleId = request.UserRoleId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}