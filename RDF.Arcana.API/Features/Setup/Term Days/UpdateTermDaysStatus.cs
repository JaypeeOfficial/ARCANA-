using FlexLabs.EntityFrameworkCore.Upsert.Runners;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

public class UpdateTermDaysStatus
{
    public class UpdateTermDaysStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateTermDaysStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTermDaysStatusCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingTermDays is null)
            {
                throw new TermDaysNotFoundException();
            }

            existingTermDays.IsActive = request.Status;
            existingTermDays.UpdatedAt = DateTime.Now;
            existingTermDays.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}