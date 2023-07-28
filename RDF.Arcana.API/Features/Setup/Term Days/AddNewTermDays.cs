using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

public class AddNewTermDays
{
    public class AddNewTermDaysCommand : IRequest<Unit>
    {
        public int Days { get; set; }
        public string AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewTermDaysCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewTermDaysCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Days == request.Days, cancellationToken);

            if (existingTermDays is not null)
            {
                throw new TermDaysAlreadyExist();
            }

            var termDays = new TermDays
            {
                Days = request.Days,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.TermDays.AddAsync(termDays, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}