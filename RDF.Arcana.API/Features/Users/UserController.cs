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

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUsersAsync([FromQuery]GetUsersAsync.GetUserAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var users = await _mediatR.Send(query);
            
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
}