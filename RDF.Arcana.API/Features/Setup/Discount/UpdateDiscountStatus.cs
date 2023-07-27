using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

public class UpdateDiscountStatus
{
    public class UpdateDiscountStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string ModiefiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDiscountStatusCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount =
                await _context.Discounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingDiscount is null)
            {
                throw new DiscountNotFoundException();
            }

            existingDiscount.IsActive = request.Status;
            existingDiscount.UpdateAt = DateTime.Now;
            existingDiscount.ModifiedBy = request.ModiefiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}