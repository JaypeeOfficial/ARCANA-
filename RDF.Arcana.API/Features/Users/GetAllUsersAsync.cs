using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Users;

public class GetAllUsersAsync
{
    public class GetAllUserAsyncQuery : UserParams, IRequest<PagedList<GetAllUserAsyncResult>> {}

    public class GetAllUserAsyncResult
    {
        public string? Fullname { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? RoleName { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllUserAsyncQuery, PagedList<GetAllUserAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllUserAsyncResult>> Handle(GetAllUserAsyncQuery request, CancellationToken cancellationToken)
        {
            var user =  _context.Users
                .Include(c => c.Company)
                .Include(d => d.Department)
                .Include(l => l.Location)
                .Include(r => r.Role);

            var result = user.Select(x => x.ToGetAllUserAsyncResult());

            return await PagedList<GetAllUserAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}