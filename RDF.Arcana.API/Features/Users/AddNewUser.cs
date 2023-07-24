using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Setup.Department.Exception;
using RDF.Arcana.API.Features.Setup.Role.Exception;
using RDF.Arcana.API.Features.Users.Exception;

namespace RDF.Arcana.API.Features.Users;

public class AddNewUser
{
    public class AddNewUserCommand : IRequest<Unit>
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }


        public class Handler : IRequestHandler<AddNewUserCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddNewUserCommand command, CancellationToken cancellationToken)
            {
                var validateExistingUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);
                var validateCompany =
                    await _context.Companies.AnyAsync(x => x.Id == command.CompanyId, cancellationToken);
                var validateRole =
                    await _context.Roles.AnyAsync(x => x.Id == command.RoleId, cancellationToken);
                var validateDepartments =
                    await _context.Departments.AnyAsync(x => x.Id == command.DepartmentId,
                        cancellationToken);

                if (!validateCompany)
                {
                    throw new NoCompanyFoundException();
                }

                if (!validateRole)
                {
                    throw new NoRoleFoundException();
                }

                if (!validateDepartments)
                {
                    throw new NoDepartmentFoundException();
                }

                if (validateExistingUser is not null) throw new UserAlreadyExistException(command.Username);

                var user = new User
                {
                    Fullname = command.Fullname,
                    Username = command.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                    CompanyId = command.CompanyId,
                    LocationId = command.LocationId,
                    DepartmentId = command.DepartmentId,
                    RoleId = command.RoleId,
                    IsActive = true,
                };

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}