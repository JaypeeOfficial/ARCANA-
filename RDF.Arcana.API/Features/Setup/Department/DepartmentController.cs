using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Features.Setup.Company;

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
   public async Task<IActionResult> AddNewDepartment(AddNewDepartment.AddNewDepartmentCommand[] command)
   {
      var response = new QueryOrCommandResult<object>();
      foreach (var request in command)
      {
         try
         {
            await _mediator.Send(request);
            response.Success = true;
            response.Messages.Add("Department successfully added");

         }
         catch (System.Exception e)
         {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
         }
      }

      return Ok(response);
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

   [HttpGet("GetAllDepartments")]
   public async Task<IActionResult> GetAllDepartment(GetDepartmentAsync.GetDepartmentAsyncQuery request)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         var departments = await _mediator.Send(request);

         Response.AddPaginationHeader(
            departments.CurrentPage,
            departments.PageSize,
            departments.TotalCount,
            departments.TotalPages,
            departments.HasPreviousPage,
            departments.HasNextPage
         );

         var results = new QueryOrCommandResult<object>
         {
            Success = true,
            Data = new
            {
               departments,
               departments.CurrentPage,
               departments.PageSize,
               departments.TotalCount,
               departments.TotalPages,
               departments.HasPreviousPage,
               departments.HasNextPage
            }
         };
         return Ok(results);
      }
      catch (System.Exception e)
      {
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpGet("GetAllDepartmentByStatus")]
   public async Task<IActionResult> GetAllDepartmentByStatus(
      GetDepartmentsByStatus.GetAllDepartmentByStatusAsyncQuery command)
   {
      try
      {
         var department = await _mediator.Send(command);

         Response.AddPaginationHeader(
            department.CurrentPage,
            department.PageSize,
            department.TotalCount,
            department.TotalPages,
            department.HasPreviousPage,
            department.HasNextPage
         );

         var results = new QueryOrCommandResult<object>
         {
            Success = true,
            Data = new
            {
               department,
               department.CurrentPage,
               department.PageSize,
               department.TotalCount,
               department.TotalPages,
               department.HasPreviousPage,
               department.HasNextPage
            }
         };

         return Ok(results);
      }
      catch (System.Exception e)
      {
         return Conflict(e.Message);
      }
   }
}