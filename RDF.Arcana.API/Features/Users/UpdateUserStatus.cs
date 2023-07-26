using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

public class UpdateUserStatus
{
    public class UpdateUserStatusCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUserStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (existingUser is null)
                throw new NoUserFoundException();

            existingUser.IsActive = request.Status;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}