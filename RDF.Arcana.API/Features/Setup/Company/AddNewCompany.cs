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
        public bool IsActive { get; set; }
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
            
                var validateCompany = await _context.Companies.FirstOrDefaultAsync(
                    x => x.CompanyName == command.CompanyName,
                    cancellationToken: cancellationToken);

                if (validateCompany is not null)
                {
                    throw new CompanyAlreadyExists(command.CompanyName);
                }

                var companies = new Domain.Company
                {
                    CompanyName = command.CompanyName,
                    IsActive = command.IsActive
                };

                await _context.Companies.AddAsync(companies, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}