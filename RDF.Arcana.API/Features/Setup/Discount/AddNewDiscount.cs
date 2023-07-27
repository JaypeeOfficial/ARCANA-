using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

public class AddNewDiscount
{
    public class AddNewDiscountCommand : IRequest<Unit>
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
    }
    public class Handler : IRequestHandler<AddNewDiscountCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewDiscountCommand request, CancellationToken cancellationToken)
        {
            var overlapExists = await _context.Discounts
                .AnyAsync(x => x.LowerBound <= request.LowerBound && x.UpperBound >= request.LowerBound ||
                               x.LowerBound <= request.UpperBound && x.UpperBound >= request.UpperBound ||
                               x.LowerBound >= request.LowerBound && x.UpperBound <= request.UpperBound,
                    cancellationToken);

            if (overlapExists)
            {
                throw new DiscountOverlapsToTheExistingOneException();
            }
            
            var discount = new Domain.Discount
            {
                LowerBound = request.LowerBound,
                UpperBound = request.UpperBound,
                CommissionRateLower = request.CommissionRateLower,
                CommissionRateUpper = request.CommissionRateUpper,
                IsActive = true
            };

            await _context.Discounts.AddAsync(discount, cancellationToken);
    
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}