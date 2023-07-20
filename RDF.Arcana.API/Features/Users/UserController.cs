using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Users;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IMediator _mediatR;

    public UserController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    [HttpPost]
    [Route("AddNewUser")]
    public async Task<ActionResult<QueryOrCommandResult<object>>> AddNewUser([FromBody]AddNewUser.AddNewUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediatR.Send(command);
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

    [HttpGet("GetAllUser")]
    public async Task<IActionResult> GetAllUser([FromQuery]GetAllUsersAsync.GetAllUserAsyncQuery request)
    {
        try
        {
            var users = await _mediatR.Send(request);
            Response.AddPaginationHeader(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages,
                users.HasPreviousPage,
                users.HasNextPage);

            var result = new QueryOrCommandResult<object>
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
            
            result.Messages.Add("Successfully Fetch");
            return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UpdateUser.UpdateUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediatR.Send(command);
            response.Success = true;
            response.Messages.Add("User successfully updated");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}