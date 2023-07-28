using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

[Route("api/[controller]")]
[ApiController]

public class TermDaysController : Controller
{
    private readonly IMediator _mediator;

    public TermDaysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewTermDays")]
    public async Task<IActionResult> AddNewTermDays(AddNewTermDays.AddNewTermDaysCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Term day has been added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    [HttpPut("UpdateTermDays/{id:int}")]
    public async Task<IActionResult> UpdateTermDays([FromBody]UpdateTermDays.UpdateTermDaysCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Term days has been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateTermDaysStatus/{id:int}")]
    public async Task<IActionResult> UpdateTermDaysStatus(
        [FromBody] UpdateTermDaysStatus.UpdateTermDaysStatusCommand command, [FromQuery] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            response.Messages.Add("Term Days status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpGet("GetTermDays")]
    public async Task<IActionResult> GetTermDays([FromQuery]GetTermDaysAsync.GetTermDaysAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var termDays = await _mediator.Send(query);
            Response.AddPaginationHeader(
                termDays.CurrentPage,
                termDays.PageSize,
                termDays.TotalCount,
                termDays.TotalPages,
                termDays.HasPreviousPage,
                termDays.HasNextPage
                );
            var result = new QueryOrCommandResult<object>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Data = new
                {
                    termDays,
                    termDays.CurrentPage,
                    termDays.PageSize,
                    termDays.TotalCount,
                    termDays.TotalPages,
                    termDays.HasPreviousPage,
                    termDays.HasNextPage
                }
            };
            
            result.Messages.Add("Successfully fetch data");
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