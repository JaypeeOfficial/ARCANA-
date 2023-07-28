using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Term_Days
{
    public class UpdateTermDays
    {
        public class UpdateTermDaysCommand : IRequest<Unit>
        {
            public int Id { get; set; }
            public int Days { get; set; }
            public string ModifiedBy { get; set; }
        }

        public class Handler : IRequestHandler<UpdateTermDaysCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTermDaysCommand request, CancellationToken cancellationToken)
            {
                var existingTermDays =
                    await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (existingTermDays is null)
                {
                    throw new TermDaysNotFoundException();
                }

                if (existingTermDays.Days == request.Days)
                {
                    throw new Exception("No changes");
                }

                var isTermDaysAlreadyExist = await _context.TermDays
                    .AnyAsync(x => x.Id != request.Id && x.Days == request.Days, cancellationToken);

                if (isTermDaysAlreadyExist)
                {
                    throw new TermDaysAlreadyExist();
                }

                existingTermDays.Days = request.Days;
                existingTermDays.ModifiedBy = request.ModifiedBy;
                existingTermDays.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}