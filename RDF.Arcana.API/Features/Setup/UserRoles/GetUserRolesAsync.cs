using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class GetUserRolesAsync
{
    public class GetUserRoleAsyncQuery : UserParams, IRequest<PagedList<GetUserRoleAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetUserRoleAsyncResult
    {
        public string RoleName { get; set; }
        public ICollection<string> Permissions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
    }
    
    public class Handler : IRequestHandler<GetUserRoleAsyncQuery, PagedList<GetUserRoleAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUserRoleAsyncResult>> Handle(GetUserRoleAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable <Domain.UserRoles> userRoles = _context.UserRoles.Include(x => x.User);

            if (!string.IsNullOrEmpty(request.Search))
            {
                userRoles = userRoles.Where(x => x.RoleName.Contains(request.Search));
            }

            if (request.Status is not null)
            {
                userRoles = userRoles.Where(x => x.IsActive == request.Status);
            }

            var result = userRoles.Select(x => x.ToGetUserRoleAsyncQueryResult());

            return await PagedList<GetUserRoleAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}