using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

public class ChangeUserPassword
{
    public class ChangeUserPasswordCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
    
    public class Handler : IRequestHandler<ChangeUserPasswordCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NoUserFoundException();
            }
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}