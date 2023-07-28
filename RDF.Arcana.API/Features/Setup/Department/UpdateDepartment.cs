using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Company;
using RDF.Arcana.API.Features.Setup.Department.Exception;
using RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

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
             
             var validateDepartmentName =
                 await _context.Departments.Where(x => x.DepartmentName == request.DepartmentName).FirstOrDefaultAsync(cancellationToken);

             if (validateDepartmentName is null)
             {
                 validateDepartment.DepartmentName = request.DepartmentName;
                 validateDepartment.UpdatedAt = DateTime.Now;

                 await _context.SaveChangesAsync(cancellationToken);
                 return Unit.Value;

             }
            
             if (validateDepartmentName.DepartmentName == request.DepartmentName && validateDepartmentName.Id == request.DepartmentId)
             {
                 throw new System.Exception("No changes");
             }
            
             if (validateDepartmentName.DepartmentName == request.DepartmentName && validateDepartmentName.Id != request.DepartmentId)
             {
                 throw new DepartmentAlreadyExistException(request.DepartmentName);
             }
             
             return Unit.Value;
         }
     }
}