using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

public class AddNewDepartment
{
    public class AddNewDepartmentCommand : IRequest<Unit>
    {
        public string DepartmentName { get; set; }
    }
    public class Handler : IRequestHandler<AddNewDepartmentCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validateDepartment = await _context.Departments.SingleOrDefaultAsync(
                x => x.DepartmentName == request.DepartmentName,
                cancellationToken);
            
            
            if (validateDepartment is not null)
            {
                throw new NoDepartmentFoundException();
                
            }

            var department = new Domain.Department
            {
                DepartmentName = request.DepartmentName,
                IsActive = true
            };
            
            await _context.Departments.AddAsync(department, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}