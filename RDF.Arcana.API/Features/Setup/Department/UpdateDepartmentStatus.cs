using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

public class UpdateDepartmentStatus
{
    public class UpdateDepartmentStatusCommand : IRequest<Unit>
    {
        public int DepartmentId { get; set; }
        public bool IsActive { get; set; }
    }
    public class Handler : IRequestHandler<UpdateDepartmentStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDepartmentStatusCommand request, CancellationToken cancellationToken)
        {
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            if (validateDepartment is null)
            {
                throw new NoDepartmentFoundException();
            }

            validateDepartment.IsActive = request.IsActive;
            validateDepartment.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}