using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Role;

public class GetRolesAsync
{
    public class GetRolesAsyncQuery : UserParams, IRequest<PagedList<GetRolesAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetRolesAsyncResult
    {
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public bool IsActive { get; set; }
    }

    public class Users
    {
        public string Fullname { get; set; }
    }
    public class Handler : IRequestHandler<GetRolesAsyncQuery, PagedList<GetRolesAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetRolesAsyncResult>> Handle(GetRolesAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Role> roles = _context.Roles.Include(x => x.Users);
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                roles = roles.Where(x => x.RoleName.Contains(request.Search));
            }

            if (request.Status != null)
            {

                roles = roles.Where(x => x.IsActive == request.Status);
            }

            var result = roles.Select(x => x.ToGetAllRolesAsyncResult());

            return await PagedList<GetRolesAsyncResult>.CreateAsync(result, request.PageSize, request.PageNumber);
        }
    }
}