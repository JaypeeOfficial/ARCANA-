using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Company;

public class UpdateCompanyStatus
{
    public class UpdateCompanyStatusCommand : IRequest<Unit>
    {
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<UpdateCompanyStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompanyStatusCommand request, CancellationToken cancellationToken)
        {
            var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

            if (validateCompany is null)
            {
                throw new NoCompanyFoundException();
            }

            validateCompany.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}