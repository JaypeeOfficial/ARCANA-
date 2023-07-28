using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/[controller]")]
[ApiController]

public class UserRoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewUserRole")]
    public async Task<IActionResult> AddNewUserRoles(AddNewUserRoles.AddNewUserRolesCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("User Role has been added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    [HttpPut("UpdateUserRole/{id:int}")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] int id, [FromBody]UpdateUserRole.UpdateUserRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserRoleId = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("User Role has been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateUserRoleStatus/{id:int}")]
     public async Task<IActionResult> UpdateUserRoleStatus([FromRoute] int id)
     {
         var response = new QueryOrCommandResult<object>();
         try
         {
             var command = new UpdateUserRoleStatus.UpdateUserRoleStatusCommand
             {
                 UserRoleId = id,
                 ModifiedBy = User.Identity?.Name
             };
             await _mediator.Send(command);
             response.Status = StatusCodes.Status200OK;
             response.Success = true;
             response.Messages.Add("User Role status has been successfully updated");
             return Ok(response);
         }
         catch (Exception e)
         {
             response.Status = StatusCodes.Status409Conflict;
             response.Messages.Add(e.Message);
             return Conflict(response);
         }
     }

    [HttpPut("UntagUserRole/{id:int}")]
    public async Task<IActionResult> UntagUserRolePermission([FromRoute] int id, [FromBody]UntagAndTagUserRolePermission.UntagAndTagUserRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserRoleId = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("User Role has been successfully untagged");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    [HttpGet("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles([FromQuery]GetUserRolesAsync.GetUserRoleAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var userRoles = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                userRoles.CurrentPage,
                userRoles.PageSize,
                userRoles.TotalCount,
                userRoles.TotalPages,
                userRoles.HasPreviousPage,
                userRoles.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    userRoles,
                    userRoles.CurrentPage,
                    userRoles.PageSize,
                    userRoles.TotalCount,
                    userRoles.TotalPages,
                    userRoles.HasPreviousPage,
                    userRoles.HasNextPage
                }
            };
            
            response.Messages.Add("Successfully fetch data");
            return Ok(result);

        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}