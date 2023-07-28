using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

public class UpdateDiscount
{
    public class UpdateDiscountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }
        
       public async Task<Unit> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = await _context.Discounts.FindAsync(request.Id);

            if (discount == null)
                throw new DiscountNotFoundException();

            var overlapExists = await _context.Discounts
                .Where(x => x.Id != request.Id)
                .AnyAsync(x => (x.LowerBound <= request.LowerBound && x.UpperBound >= request.LowerBound) || 
                               (x.LowerBound <= request.UpperBound && x.UpperBound >= request.UpperBound) ||
                               (x.LowerBound >= request.LowerBound && x.UpperBound <= request.UpperBound),
                    cancellationToken);

            if (overlapExists)
                throw new DiscountOverlapsToTheExistingOneException();

            discount.LowerBound = request.LowerBound;
            discount.UpperBound = request.UpperBound;
            discount.CommissionRateLower = request.CommissionRateLower;
            discount.CommissionRateUpper = request.CommissionRateUpper;
            discount.ModifiedBy = request.ModifiedBy;
            discount.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}