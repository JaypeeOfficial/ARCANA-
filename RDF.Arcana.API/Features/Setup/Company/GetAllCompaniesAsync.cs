using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Company;

public class GetAllCompaniesAsync
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<GetAllCompaniesResult>>{}

    public class GetAllCompaniesResult
    {
        public string? CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<GetAllCompaniesResult>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCompaniesResult>> Handle(GetAllCompaniesQuery request,
            CancellationToken cancellationToken)
        {
            var companies = await _context.Companies.ToListAsync(cancellationToken);
            var result = companies.Select(c => c.ToGetAllCompaniesResult());
            return result;
        }
    }
}