using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

public class UpdateDepartment
{
     public class UpdateDepartmentCommand : IRequest<Unit>
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
     
     public class Handler : IRequestHandler<UpdateDepartmentCommand, Unit>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
         {
             var validateDepartment =
                 await _context.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId,
                     cancellationToken);
             if (validateDepartment is null)
             {
                 throw new NoDepartmentFoundException();
             }

             validateDepartment.DepartmentName = request.DepartmentName;
             validateDepartment.UpdatedAt = DateTime.Now;

             await _context.SaveChangesAsync(cancellationToken);
             return Unit.Value;
         }
     }
}