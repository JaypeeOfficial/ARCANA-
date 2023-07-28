using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Company;

public class UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<Unit>
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
 
    public class Handler : IRequestHandler<UpdateCompanyCommand, Unit>
    {
        private readonly DataContext _context;
 
        public Handler(DataContext context)
        {
            _context = context;
        }
 
        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(
                x => x.Id == request.CompanyId, cancellationToken
            );
 
            if (existingCompany is null)
            {
                throw new NoCompanyFoundException();
            }
 
            if (existingCompany.CompanyName == request.CompanyName)
            {
                throw new Exception("No changes");
            }
 
            var isCompanyAlreadyExist = await _context.Companies
                .AnyAsync(x => x.Id != request.CompanyId && x.CompanyName == request.CompanyName, cancellationToken);
 
            if (isCompanyAlreadyExist)
            {
                throw new CompanyAlreadyExists(request.CompanyName);
            }
 
            existingCompany.CompanyName = request.CompanyName;
            existingCompany.UpdatedAt = DateTime.UtcNow;
 
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}