using System.Reflection.Metadata;
using AutoMapper;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.Company;

public class AddNewCompany
{
    public class AddNewCompanyCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; }
    }

    public class Handler : IRequestHandler<AddNewCompanyCommand, Unit>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddNewCompanyCommand command, CancellationToken cancellationToken)
        {
            var existingCompany =
                await _context.Companies.FirstOrDefaultAsync(c => c.CompanyName == command.CompanyName,
                    cancellationToken);

            if (existingCompany != null)
            {
                throw new NoCompanyFoundException();
            }

            var company = new Domain.Company
            {
                CompanyName = command.CompanyName,
                IsActive = true
            };

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}