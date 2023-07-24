using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;

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

    [HttpPost("AddNewUserRoles")]
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
}