using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

public class GetDepartmentAsync
{
    public class GetDepartmentAsyncQuery : UserParams, IRequest<PagedList<GetDepartmentAsyncResult>>
    {
        public bool Status { get; set; }
        public string? Search { get; set; }
    }

    public class GetDepartmentAsyncResult
    {
        public string? DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    public class Handler : IRequestHandler<GetDepartmentAsyncQuery, PagedList<GetDepartmentAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDepartmentAsyncResult>> Handle(GetDepartmentAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Department> department = _context.Departments;
            if (department is null)
            {
                throw new NoDepartmentFoundException();
            }

            department = !string.IsNullOrEmpty(request.Search)
                ? department
                    .Where(d => d.DepartmentName.Contains(request.Search))
                : department
                    .Where(d => d.IsActive == request.Status);

            var result = department.Select(d => d.ToGetAllDepartmentAsyncResult());

            return await PagedList<GetDepartmentAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
}