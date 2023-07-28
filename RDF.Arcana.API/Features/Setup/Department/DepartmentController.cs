using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/Department")]
[ApiController]

public class DepartmentController : ControllerBase
{
   private readonly IMediator _mediator;

   public DepartmentController(IMediator mediator)
   {
      _mediator = mediator;
   }

   

   

   
}