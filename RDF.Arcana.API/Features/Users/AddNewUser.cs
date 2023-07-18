using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Users.Exception;

namespace RDF.Arcana.API.Features.Users;

public class AddNewUser
{
    public class AddNewUserCommand : IRequest<Unit>
    {
        public AddNewUserCommand(string fullname, string username, string password)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
        }

        public string Fullname { get; }
        public string Username { get; }
        public string Password { get; }


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

                if (validateExistingUser is not null) throw new UserAlreadyExistException(command.Username);

                var user = new User
                {
                    Fullname = command.Fullname,
                    Username = command.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                    IsActive = true
                };

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}