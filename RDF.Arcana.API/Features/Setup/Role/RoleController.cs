using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Features.Setup.Role.Exception;

namespace RDF.Arcana.API.Features.Setup.Role;
[Route("api/[controller]")]
[ApiController]

public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewRole")]
    public async Task<IActionResult> AddNewRole(AddNewRole.AddNewRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Role added successfully");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(UpdateRole.UpdateRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            return Ok(response);

        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }

    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetRoles([FromQuery]GetRolesAsync.GetRolesAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var roles = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                roles.CurrentPage,
                roles.PageSize,
                roles.TotalCount,
                roles.TotalPages,
                roles.HasPreviousPage,
                roles.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Data = new
                {
                    roles,
                    roles.CurrentPage,
                    roles.PageSize,
                    roles.TotalCount,
                    roles.TotalPages,
                    roles.HasPreviousPage,
                    roles.HasNextPage
                }
            };
            return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }

    [HttpPatch("UpdateRoleStatus")]
    public async Task<IActionResult> UpdateRoleStatus(UpdateRoleStatus.UpdateRoleStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("The Role status has been updated successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}