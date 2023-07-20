using System.Data;
using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Department;

public class GetDepartmentsByStatus
{
    public class GetAllDepartmentByStatusAsyncQuery : UserParams, IRequest<PagedList<GetAllDepartmentByStatusResult>>
    {
        public bool Status { get; set; }
    }

    public class GetAllDepartmentByStatusResult
    {
        public string? DepartmentName { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllDepartmentByStatusAsyncQuery, PagedList<GetAllDepartmentByStatusResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllDepartmentByStatusResult>> Handle(GetAllDepartmentByStatusAsyncQuery request, CancellationToken cancellationToken)
        {
            var departments = _context.Departments.Where(x => x.IsActive == request.Status);

            var results = departments.Select(c => c.ToGetAllDepartmentByStatusResult());

            return await PagedList<GetAllDepartmentByStatusResult>.CreateAsync(results, request.PageNumber,
                request.PageNumber);
        }
    }
   
    
}