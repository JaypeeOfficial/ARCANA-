using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/[controller]")]
[ApiController]
    
public class DepartmentController : ControllerBase
{
   private readonly IMediator _mediator;

   public DepartmentController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpPost("AddNewDepartment")]
   public async Task<IActionResult> AddNewDepartment(AddNewDepartment.AddNewDepartmentCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Success = true;
         response.Messages.Add("Department successfully added");
         return Ok(response);
      }
      catch (System.Exception e)
      {
         response.Success = false;
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpPut("UpdateDepartment")]
   public async Task<IActionResult> UpdateDepartment(UpdateDepartment.UpdateDepartmentCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Success = true;
         response.Messages.Add("Department successfully updated");
         return Ok(response);
      }
      catch (System.Exception e)
      {
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpPatch("UpdateDepartmentStatus")]
   public async Task<IActionResult> UpdateDepartmentStatus(UpdateDepartmentStatus.UpdateDepartmentStatusCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Success = true;
         response.Messages.Add("Status successfully updated");
         return Ok(response);
      }
      catch (System.Exception e)
      {
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }
}