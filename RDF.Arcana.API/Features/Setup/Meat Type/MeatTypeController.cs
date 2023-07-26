using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/[controller]")]
[ApiController]

public class MeatTypeController : Controller
{
    private readonly IMediator _mediator;

    public MeatTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewMeatType")]
    public async Task<IActionResult> AddNewMeatType(AddNewMeatType.AddNewMeatTypeCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Meat Type successfully added");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}