using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var validateCompany = await _context.Companies.FirstOrDefaultAsync(
                x => x.Id == request.CompanyId, cancellationToken
            );

            if (validateCompany == null )
            {
                throw new NoCompanyFoundException();
            }

            validateCompany.CompanyName = request.CompanyName;
            validateCompany.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}