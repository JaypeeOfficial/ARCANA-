using MediatR;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Users;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IMediator _mediatr;

    public UserController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    [Route("AddNewUser")]
    public async Task<ActionResult<QueryOrCommandResult<object>>> AddNewUser([FromBody]AddNewUser.AddNewUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediatr.Send(command);
            response.Success = true;
            response.Data = result;
            response.Messages.Add("User added successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUsersAsync([FromQuery]GetUsersAsync.GetUserAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var users = await _mediatr.Send(query);
            
            Response.AddPaginationHeader(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages,
                users.HasPreviousPage,
                users.HasNextPage
                );

            var result = new QueryOrCommandResult<object>()
            {
                Success = true,
                Data = new
                {
                    users,
                    users.CurrentPage,
                    users.PageSize,
                    users.TotalCount,
                    users.TotalPages,
                    users.HasPreviousPage,
                    users.HasNextPage
                }
            };
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Successfully fetch data");
            return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("ChangeUserPassword/{id:int}")]
    public async Task<IActionResult> ChangeUserPassword([FromRoute] int id, [FromBody] ChangeUserPassword.ChangeUserPasswordCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserId = id;
            await _mediatr.Send(command);
            response.Messages.Add("Password has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody]UpdateUser.UpdateUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserId = id;
            await _mediatr.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("User has been updated successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateUserStatus/{id}")]
    public async Task<IActionResult> UpdateUserStatus([FromRoute] int id, [FromBody]UpdateUserStatus.UpdateUserStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserId = id;
            await _mediatr.Send(command);
            response.Messages.Add("User status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Conflict(response);
        }
    }
}