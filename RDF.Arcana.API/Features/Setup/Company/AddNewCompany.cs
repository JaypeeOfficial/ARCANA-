using System.Reflection.Metadata;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.Company;

public class AddNewCompany
{
    public class AddNewCompanyCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; }
        public bool Status { get; set; }
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
                await _context.Companies.SingleOrDefaultAsync(c => c.CompanyName == command.CompanyName,
                    cancellationToken);

            if (existingCompany != null)
            {
                existingCompany.IsActive = command.Status;
                _context.Companies.Attach(existingCompany).State = EntityState.Modified;
            }
            else
            {
                var company = new Domain.Company
                {
                    CompanyName = command.CompanyName,
                    IsActive = command.Status
                };

                await _context.Companies.AddAsync(company, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}