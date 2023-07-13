using MediatR;
using Microsoft.AspNetCore.Mvc;
using MineralWaterMonitoring.Common;

namespace RDF.Arcana.API.Features.Users;

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
}