using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company.Exceptions;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/Company")]
[ApiController]

public class AddNewCompany : ControllerBase
{

    private readonly IMediator _mediator;

    public AddNewCompany(IMediator mediator)
    {
        _mediator = mediator;
    }


    public class AddNewCompanyCommand : IRequest<Unit>
    {
        public string CompanyName { get; set; }
        public string AddedBy { get; set; }
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
                AddedBy = command.AddedBy,
                IsActive = true
            };

            await _context.Companies.AddAsync(company, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    [HttpPost]
    [Route("AddNewCompany")]
    public async Task<IActionResult> AddCompany(AddNewCompanyCommand command)
    {
        try
        {
            command.AddedBy = User.Identity?.Name;
            await _mediator.Send(command);
            return Ok("Successfully added!");
                
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}